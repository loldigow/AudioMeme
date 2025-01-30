using Microsoft.VisualBasic;
using NAudio.Wave;
using SonsDeMemes.Custons;
using System.ComponentModel;


namespace SonsDeMemes
{
    public partial class AplicacaoSons : Form
    {
        private bool _closeRequested;
        private WaveOutEvent outputDevice;
        private Mp3FileReader mp3Reader;
        public AplicacaoSons()
        {
            InitializeComponent();
            InicializeIconeMinimizado();
            for (int i = 1; i <= 9; i++)
            {
                var viewModel = new BotaoModel { Nome = $"Botao {i}" };
                var botao = this.Controls.Find($"button{i}", true).FirstOrDefault() as MeuBotao;
                botao.Click += Botao_Click;
                botao.Model = viewModel;
            }
        }

        private void Botao_Click(object? sender, EventArgs e)
        {
            if (sender is MeuBotao botao && botao.Model is BotaoModel model)
            {
                if (model.Som != null)
                {
                    try
                    {
                        if (model.Running)
                        {
                            outputDevice?.Stop();
                            outputDevice?.Dispose();
                            mp3Reader?.Dispose();
                            model.Running = false;
                        }
                        else
                        {
                            outputDevice?.Stop();
                            outputDevice?.Dispose();
                            mp3Reader?.Dispose();
                            MemoryStream ms = new MemoryStream(model.Som);
                            mp3Reader = new Mp3FileReader(ms);
                            outputDevice = new WaveOutEvent();
                            outputDevice.PlaybackStopped += (sender, args) =>
                            {
                                model.Running = false;
                            };
                            outputDevice.Init(mp3Reader);
                            outputDevice.Play();
                            model.Running = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao tentar reproduzir o áudio: {ex.Message}");
                    }
                }
                else
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "MP3 Files (*.mp3)|*.mp3|All Files (*.*)|*.*";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        byte[] audioBytes = File.ReadAllBytes(filePath);
                        string nomeMusica = Interaction.InputBox("Digite o nome da música:", "Nome da Música", fileName);
                        model.Nome = nomeMusica;
                        model.Som = audioBytes;
                        botao.BackColor = Color.Green;
                        botao.Text = nomeMusica;
                    }
                }
            }
        }

        private void OutputDevice_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void InicializeIconeMinimizado()
        {
            iconMinimizado.BalloonTipTitle = "Meme sons";
            iconMinimizado.BalloonTipText = "Meme sons";
            iconMinimizado.Text = "Aplicativo para gerar memes de sons";
            iconMinimizado.Visible = true;
            iconMinimizado.ContextMenuStrip = new ContextMenuStrip();


            var toolTripETL = new ToolStripMenuItem("Meme sons");
            toolTripETL.DropDownItems.Add("Abrir Painel", null, (e, f) => { MostrePainel(e, f); });

            iconMinimizado.ContextMenuStrip.Items.Add(toolTripETL);

            iconMinimizado.ContextMenuStrip.Items.Add("Finalizar", null, (e, f) =>
            {
                _closeRequested = true;
                System.Windows.Forms.Application.Exit();
            });
            Hide();
        }

        private void MostrePainel(object? e, EventArgs f)
        {
            Show();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_closeRequested)
            {
                outputDevice?.Stop();
                outputDevice?.Dispose();
                mp3Reader?.Dispose();
                e.Cancel = false;
                return;
            }

            e.Cancel = true;
            InicializeIconeMinimizado();
        }
    }

    public class BotaoModel
    {
        public string Nome { get; set; }
        public byte[] Som { get; set; }
        public bool Running { get; set; }
    }
}

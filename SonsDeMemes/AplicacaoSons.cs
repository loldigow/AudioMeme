using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
using NAudio.Wave;
using Newtonsoft.Json;
using SonsDeMemes.Custons;
using System.ComponentModel;
using System.Reflection;
using System.Xml;


namespace SonsDeMemes
{
    public partial class AplicacaoSons : Form
    {
        private bool _closeRequested;
        private WaveOutEvent outputDevice;
        private Mp3FileReader mp3Reader;
        private List<BotaoModel> _modelos = new List<BotaoModel>();
        private static string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "audios.json");


        public AplicacaoSons()
        {
            InitializeComponent();
            InicializeIconeMinimizado();
            CarregueAudios(); 
            foreach (var control in this.Controls)
            {
                if (control is MeuBotao botao && botao.Name.Contains("btnMusica"))
                {
                    var sequencia = new string(botao.Name.Where(char.IsDigit).ToArray());
                    if(int.TryParse(sequencia, out int i))
                    {
                        var musica = _modelos.FirstOrDefault(x => x.Sequencia == i) ?? new BotaoModel { Nome = $"<vazio>", Sequencia = i };
                        botao.MouseUp += Botao_MouseUp;
                        botao.Model = musica;
                        botao.BackColor = musica.Backgroud;
                        botao.Text = musica.Nome;
                    }

                    //musica = _modelos.FirstOrDefault(x => x.Sequencia == ++i) ?? new BotaoModel { Nome = $"Botao {botao.Name}", Sequencia = i }; ;
                    //botao.MouseUp += Botao_MouseUp; // Adiciona o evento MouseUp
                    //botao.Model = musica;
                }
            }
        }

        private void CarregueAudios()
        {
            if (!File.Exists(jsonFilePath)) 
                return;

            string json = File.ReadAllText(jsonFilePath);
            _modelos = JsonConvert.DeserializeObject<List<BotaoModel>>(json);
        }

        private void Botao_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Botao_Click(sender, e);
            }
            else
            {
                AdicioneOuSubstituaSom(sender, e);
            }
        }

        private void AdicioneOuSubstituaSom(object? sender, EventArgs e)
        {
            if (sender is MeuBotao botao && botao.Model is BotaoModel model)
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
                    botao.BackColor = Color.LightGray;
                    model.Backgroud = Color.LightGray;
                    botao.Text = nomeMusica;

                    SalveMusicaLocal(model);

                }
            }
        }

        private void SalveMusicaLocal(BotaoModel model)
        {
            try
            {
                var musica = _modelos.FirstOrDefault(x => x.Sequencia == model.Sequencia);
                if (musica != null)
                {
                    _modelos.Remove(musica);
                }
                _modelos.Add(model);
                string json = JsonConvert.SerializeObject(_modelos);
                File.WriteAllText(jsonFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao tentar salvar a música: {ex.Message}");
            }
        }

        private void Botao_Click(object? sender, EventArgs e)
        {
            if (sender is MeuBotao botao && botao.Model is BotaoModel model)
            {
                if (model.Som == null)
                {
                    AdicioneOuSubstituaSom(sender, e);
                    return;
                }


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
                            botao.BackColor = Color.LightGray;
                        };
                        outputDevice.Init(mp3Reader);
                        outputDevice.Play();
                        model.Running = true;
                        botao.BackColor = Color.Gray;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao tentar reproduzir o áudio: {ex.Message}");
                }
            }
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
                Application.Exit();
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
        public int Sequencia { get; set; }
        public string Nome { get; set; }
        public byte[] Som { get; set; }
        public bool Running { get; set; }
        public Color Backgroud { get; set; }
    }
}

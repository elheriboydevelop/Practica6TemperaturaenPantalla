using System.IO.Ports;
namespace PuertoSerie
{
    public partial class frmSerie : Form
    {


        SerialPort sp = new SerialPort();
        SerialPort sp2= new SerialPort();
        string[] puertos;

        public frmSerie()
        {
            InitializeComponent();
        }

        private void frmSerie_Load(object sender, EventArgs e)
        {
            actualizarPuertos();
            cmbPuertos.DataSource = puertos;

        }

        private void actualizarPuertos()
        {
            puertos = SerialPort.GetPortNames(); cmbPuertos.DataSource = SerialPort.GetPortNames();
        }
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (chkBlue.Checked)
            {

                sp2.WriteLine(txtEnviar.Text);
                txtEnviar.Clear();
            }
            else
            {
                sp.WriteLine(txtEnviar.Text);
                txtEnviar.Clear();
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkBlue.Checked)
                {
                    sp.PortName = cmbPuertos.Text;
                    sp.Open();
                    sp2.PortName = cmbPuertos2.Text;
                    sp2.Open();
                    MessageBox.Show("Conectado");
                    sp.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
                }
                else
                {
                    sp.PortName = cmbPuertos.Text;
                    sp.Open();
                    MessageBox.Show("Conectado");
                    sp.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
                }


            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            SerialPort actualSP = (SerialPort)sender;


            string inData = actualSP.ReadLine();
            rtbLog.Invoke(new MethodInvoker(
                        delegate
                        {
                            rtbLog.Text = rtbLog.Text + inData;
                        }

                        ));
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarPuertos();
            cmbPuertos.DataSource = puertos;
            cmbPuertos2.DataSource = puertos;
        }

        private void chkBlue_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBlue.Checked == true)
            {
                cmbPuertos2.Enabled = true;
                actualizarPuertos();
                cmbPuertos2.DataSource = puertos;
                label1.Text = "Puerto Serie (Entrada)";
            }
            else
            {
                cmbPuertos2.Enabled = false;
                label1.Text = "Puerto Serie";
            }
        }

        private void frmSerie_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sp.IsOpen == true) { 
                sp.Close();
            }
            if (sp2.IsOpen == true) {
                sp2.Close();
            }
        }
    }
}

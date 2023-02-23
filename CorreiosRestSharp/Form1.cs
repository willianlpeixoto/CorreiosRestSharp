using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;



namespace CorreiosRestSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bt_busca_Click(object sender, EventArgs e)
        {

            try
            {

                string sURL = "http://viacep.com.br/ws/" + txt_CEP.Text.ToString().Trim() + "/";

                RestClient restCliente = new RestClient(sURL);
                RestRequest restRequisicao = new RestRequest("json/", Method.Get);

                RestResponse restResposta = restCliente.Execute(restRequisicao);

                if (restResposta.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {

                    MessageBox.Show("Houve um problma com a requisicao." + Environment.NewLine +
                        restResposta.Content, "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                {

                    EnderecoRetorno endereco = JsonConvert.DeserializeObject<EnderecoRetorno>(restResposta.Content);

                    txtLogradouro.Text = endereco.Logradouro;
                    txtComplemento.Text = endereco.Complemento;
                    txtBairro.Text = endereco.Bairro;
                    txtLocalidade.Text = endereco.Localidade;
                    txtUF.Text = endereco.UF;
                    txtUnidade.Text = endereco.Unidade;
                    txtCodIBGE.Text = endereco.IBGE;
                    txtGia.Text = endereco.Gia;

                    if ( endereco.CEP is null)
                    {

                        MessageBox.Show("CEP não encontrado!");

                    }

                }

            }
            catch (Exception erro)
            {

                MessageBox.Show("Ocorreu um erro." + Environment.NewLine +
                    erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            txt_CEP.Text = "36880001";

        }

    }

    class EnderecoRetorno
    {

        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string Unidade { get; set; }
        public string IBGE { get; set; }
        public string Gia { get; set; }
    }

}

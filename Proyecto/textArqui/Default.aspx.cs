using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Management;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace textAnalizer
{
    public partial class _Default : Page
    {

        LinkedList<Result> listGeneralWorkedData;
        Stopwatch time;
        double fullTimeInt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            listGeneralWorkedData = new LinkedList<Result>();
        }
        public int getCoresNumber()
        {
            int coreCount = 0;
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                coreCount += int.Parse(item["NumberOfCores"].ToString());
            }
            return coreCount;
        }
        public void findInsertElementInGeneralList(int position, string data)
        {
            Boolean find = false;
            foreach (var element in this.listGeneralWorkedData)
            {
                if (element.getPosition().Equals(position))
                {
                    find = true;
                    element.addItemToList(data);                }
            }
            if (!find)
            {
                LinkedList<String> listaAux = new LinkedList<string>();
                Result firstInList = new Result();
                firstInList.setPosition(position);
                firstInList.setlistData(listaAux);
                this.listGeneralWorkedData.AddLast(firstInList);
                findInsertElementInGeneralList(position, data);
            }
        }
        public void cleanData_Click(object sender, EventArgs e)
        {
            this.initialData_txt.Text = "";
            this.resultData_txt.Text = "";
            this.resultData_txt.Enabled = false;
            mostrarOcultarMensaje("todos", "ocultar", "");
            this.fullTimeInt = 0;
        }
        public void startProcess_Click(object sender, EventArgs e)
        {
            mostrarOcultarMensaje("ocultar", "todos", "");
            String textToAnalize = this.initialData_txt.Text.ToString();
            if (textToAnalize.Equals(""))
            {
                mostrarOcultarMensaje("error", "mostrar", "You have to insert data in the left blank");
            }
            else
            {
                if (rd_secuencial.Checked)
                {
                    secuencialProccess(int.Parse(this.kindOperation_DD.SelectedValue));
                }
                else
                {
                    paralelProccess(int.Parse(this.kindOperation_DD.SelectedValue));
                }
            }
        }
        public void paralelProccess(int isEncriptation)
        {
            int cores = getCoresNumber();
            LinkedList<Result> listToWorkParallel = buildArrayFromString_Cores(this.initialData_txt.Text.ToString());
            this.fullTimeInt = 0;
            this.time = Stopwatch.StartNew();
    
            Parallel.ForEach(listToWorkParallel, punt =>
                {                
                    string fullString = "";
                    foreach ( var letter in punt.getListData()) {
                        fullString += letter;
                    }
                    setEncriptation(punt.getPosition(), fullString, Convert.ToBoolean(isEncriptation));                    
                });

           while (true)
            {
                if(cores.Equals(this.listGeneralWorkedData.Count)){
                    this.time.Stop();
                    ShowDefinedlyTime();
                    sortGeneralList();
                    showDataFromAsyncFunction();
                    break;
                }
            }
        }
        public void showDataFromAsyncFunction()
        {
            string newFullPath = "";

            foreach (var item in this.listGeneralWorkedData)
            {
                newFullPath += item.getListData().ElementAt(0);
            }

            this.resultData_txt.Text = newFullPath;
            this.resultData_txt.Enabled = true;
        }
        public Result searchPosElementGeneral(int pos)
        {
            Result result = new Result();
            foreach(var element in this.listGeneralWorkedData) {
                if (element.getPosition().Equals(pos))
                {
                    result = element;
                    break;
                }
            }
            return result;
        }
        public void sortGeneralList()
        {
            LinkedList<Result> newListGeneral = new LinkedList<Result>();

            for(int i = 0; i<this.listGeneralWorkedData.Count; i++){
                newListGeneral.AddLast(searchPosElementGeneral(i));
            }

            this.listGeneralWorkedData = newListGeneral;
        }
       public LinkedList<Result> buildArrayFromString_Cores(string toAnalize)
        {
            LinkedList<Result> listWithStringParts = new LinkedList<Result>();

            int cores = getCoresNumber();
            int accum = 0;
            int basePartition = (int)((toAnalize.Length) / cores);
            accum = basePartition;

            for(int i = 0; i < cores; i++)
            {
                int fromPosition = i * basePartition;
                Result item = new Result();

                item.setPosition(i);            
                if (i == cores - 1)
                {
                    item.addItemToList(toAnalize.Substring(fromPosition, toAnalize.Length - accum));
                }
                else
                {
                    item.addItemToList(toAnalize.Substring(fromPosition, accum));
                }

                listWithStringParts.AddLast(item);
            }
            
            return listWithStringParts;

        }
       public void ShowDefinedlyTime()
       {
           mostrarOcultarMensaje("exito", "mostrar", "Duración segundos: " + this.fullTimeInt.ToString());
       }
       public void secuencialProccess(int isEncriptation)
       {
            this.fullTimeInt = 0;
            this.time = Stopwatch.StartNew(); 
            if (isEncriptation.Equals(1))
            {                
                setEncriptation(0, this.initialData_txt.Text.ToString(), true);
            }
            else
            {
                setEncriptation(0, this.initialData_txt.Text.ToString(), false);
            }

            this.time.Stop();
            showDataToResultsBlanck(0);
            ShowDefinedlyTime();
        }
        public void showDataToResultsBlanck(int position)
        {
            String finalDataToSet = "";

            foreach(var element in this.listGeneralWorkedData ){
                if (element.getPosition().Equals(position))
                {
                    foreach(var item in element.getListData())
                    {
                        finalDataToSet += item.ToString();
                    }
                }
                this.resultData_txt.Text = finalDataToSet;
                this.resultData_txt.Enabled = true;
            }
        }
        public void setEncriptation(int position, string textToAnalize, Boolean isEncrypt)
        {
            string resultEncriptation = "";
            
            foreach (var letra in textToAnalize)
            {
                string newLetter = isEncrypt ? ((char)addEncryptation((int)letra)).ToString() : ((char)reduceEncryptation((int)letra)).ToString();
                resultEncriptation += newLetter;
            }

            this.findInsertElementInGeneralList(position, resultEncriptation);
            this.fullTimeInt += this.time.ElapsedMilliseconds * 0.001;
        }
        public int addEncryptation(int encryptedNumber)
        {
            encryptedNumber = encryptedNumber + 256;
            encryptedNumber = encryptedNumber * 4;
            encryptedNumber = encryptedNumber / 2;

            return encryptedNumber;
        }
        public int reduceEncryptation(int encryptedNumber)
        {
            return (((encryptedNumber * 2) / 4) - 256);
        }
        public void mostrarOcultarMensaje(String tipo, String vista, String msj)
        {
            if (vista.Equals("mostrar"))
            {
                if (tipo.Equals("exito"))
                {
                    panelMsjExito.Visible = true;
                    lbInd.Text = msj;
                }
                else if (tipo.Equals("advertencia"))
                {
                    panelMsjAdv.Visible = true;
                    lbIndAdv.Text = msj;
                }
                else if (tipo.Equals("loader"))
                {
                    panelMsjLoader.Visible = true;
                    lbIndLoader.Text = msj;
                }
                else
                {
                    panelMsjError.Visible = true;
                    lbIndError.Text = msj;
                }
            }
            else
            {
                if (tipo.Equals("todos"))
                {
                    panelMsjExito.Visible = false;
                    panelMsjAdv.Visible = false;
                    panelMsjError.Visible = false;
                    panelMsjLoader.Visible = false;
                }
                else if (tipo.Equals("exito"))
                    panelMsjExito.Visible = false;

                else if (tipo.Equals("advertencia"))
                    panelMsjAdv.Visible = false;

                else
                    panelMsjError.Visible = false;
            }
        }
    }
}
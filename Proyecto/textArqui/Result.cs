using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace textAnalizer
{
    public class Result
    {
        private int position = 0;
        private LinkedList<string> listData;

        public Result()
        {
            listData = new LinkedList<string>();
        }


        public void setPosition(int pos){
            this.position = pos;
        }

        public void setlistData(LinkedList<string> setLista)
        {
            this.listData = setLista;
        }

        public void addItemToList(string item)
        {
            this.listData.AddLast(item);
        }

        public int getPosition()
        {
            return this.position;
        }

        public LinkedList<string> getListData()
        {
            return this.listData;
        }
    }
}
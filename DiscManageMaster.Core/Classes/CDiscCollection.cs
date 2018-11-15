using System;
using System.Collections;
using System.Windows.Forms;

namespace DiscManageMaster.Core.Classes
{
    [Serializable]
    public class CDiscCollection : CollectionBase
    {
        public CDiscCollection(string name)
        {
            thisName = name;
            thisCreatedTime = DateTime.Now;
        }

        public int Add(CDisc cd,TreeNodeCollection tnc)
        {
            Functions.FillTreeView(cd.Folders, tnc);
            return List.Add(cd);
        }

        public int Add(CDisc cd, TreeNodeCollection tnc, int index)
        {
            Functions.FillTreeView(cd.Folders, tnc);
            List.Insert(index, cd);
            return index;
        }

        private string thisName;
        public string Name
        {
            get { return thisName; }
            set { thisName = value; }
        }

        private DateTime thisCreatedTime;
        public DateTime CreatedTime
        {
            get { return thisCreatedTime; }
            set { thisCreatedTime = value; }
        }

        public CDisc this[int index]
        {
            get { return (CDisc)List[index]; }
        }

        public bool Contains(string name)
        {
            for (int i = 0; i < Count; i++)
                if (this[i].Name == name)
                    return true;

            return false;
        }
    }
}
using System;
using System.Collections;
using System.Windows.Forms;

namespace DiscManageMaster.Core.Classes
{
    [Serializable]
    public class CCollection : CollectionBase
    {
        public int Add(CDiscCollection cdc,TreeNodeCollection tnc)
        {
            TreeNode tn = tnc.Add("", cdc.Name, CommVar.CollectionIconIndex, CommVar.CollectionIconIndex);
            int index = List.Add(cdc);
            tn.EnsureVisible();
            tn.TreeView.SelectedNode = tn;
            return index;
        }

        public int Add(CDiscCollection cdc)
        {
            return List.Add(cdc);
        }

        private string thisName;
        public string Name
        {
            get { return thisName; }
            set { thisName = value; }
        }

        private string thisMemo;
        public string Memo
        {
            get { return thisMemo; }
            set { thisMemo = value; }
        }

        public CDiscCollection this[int index]
        {
            get { return (CDiscCollection)List[index]; }
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
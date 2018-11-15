using System;
using System.Collections;
using System.Text;

namespace DiscManageMaster.Core.Classes
{
    [Serializable]
    public class CFolderCollection : CollectionBase
    {
        public int Add(CFolder cf)
        {
            return List.Add(cf);
        }

        public int Add(string name)
        {
            CFolder cf = new CFolder(name);
            return List.Add(cf);
        }

        public long Add2(CFolder cf)
        {
            List.Add(cf);
            return cf.Size;
        }

        public new void Clear()
        {
            List.Clear();
        }

        private string thisName;
        public string Name
        {
            get { return thisName; }
            set { thisName = value; }
        }

        public CFolder this[int index]
        {
            get { return (CFolder) List[index]; }
        }

        public CFolder this[string key]
        {
            get
            {
                int temp = -1;
                for(int i =0;i<Count;i++)
                {
                    if (this[i].Name == key)
                    {
                        temp = i;
                        break;
                    }
                }

                return this[temp];
            }
        }

        public bool Contains(string key)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Name == key)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

using System;
using System.Collections;
using System.Text;

namespace DiscManageMaster.Core.Classes
{
    [Serializable]
    public class CFileCollection : CollectionBase
    {
        public int Add(CFile cf)
        {
            return List.Add(cf);
        }

        public int Add(string Name)
        {
            CFile cf = new CFile(Name);
            return List.Add(cf);
        }

        public new void Clear()
        {
            List.Clear();
        }

        public CFile this[int index]
        {
            get { return (CFile)List[index]; }
        }
    }
}

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace DiscManageMaster.Core.Classes
{
    [Serializable]
    public class CRoot: Interface.IObject
    {
        private readonly CCollection thisCollection = new CCollection();
        public readonly CFolderCollection ItemCollection = new CFolderCollection();
        public COptions Options = new COptions();
        public DateTime RegDate;
        public string UserName;

        public CRoot(string name)
        {
            Name = name;
            
            thisCollection.Add(new CDiscCollection("Compact Disc"));
            thisCollection.Add(new CDiscCollection("Removable Driver"));
            thisCollection.Add(new CDiscCollection("Local Folder"));
            thisCollection.Add(new CDiscCollection("Favorites"));

            CreatedTime = DateTime.Now;
        }

        public CCollection Collection
        {
            get { return thisCollection; }
        }

        private void ClearContent()
        {
            for(int i =0;i<thisCollection.Count;i++)
            {
                for(int j = 0; j<thisCollection[i].Count;j++)
                {
                    thisCollection[i][j].IconIndex = -1;
                    ResetContent(thisCollection[i][j].Folders, thisCollection[i][j].Files);
                }
            }
        }

        private static void ResetContent(CFolderCollection c1, CFileCollection c2)
        {
            for (int i = 0; i < c2.Count; i++)
            {
                c2[i].IconIndex = -1;
                c2[i].Type = "";
            }

            foreach (CFolder cf in c1)
            {
                //if (cf.IconIndex != CommVar.CloseFolderIconIndex)
                    cf.IconIndex = -1;
                ResetContent(cf.Folders, cf.Files);
            }
        }

        public static void Save(CRoot root)
        {
            root.ClearContent();
            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data.dat", FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, root);
            }
        }

        public static void Save(CRoot root, string Path)
        {
            root.ClearContent();
            using (FileStream fs = new FileStream(Path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, root);
            }
        }

        public static CRoot Load()
        {
            try
            {
                using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Data.dat", FileMode.Open)
                    )
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (CRoot) formatter.Deserialize(fs);
                }
            }
            catch
            {
            }
            return new CRoot("Root");
        }

        public static CRoot Load(string Path)
        {
            //try
            //{
                using (FileStream fs = new FileStream(Path, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (CRoot)formatter.Deserialize(fs);
                }
            //}
            //catch
            //{
            //}
            //MessageBox.Show("Restore Error!");
            //return new CRoot("Root");
        }
    }
}
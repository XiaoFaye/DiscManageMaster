using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DiscManageMaster.Core;
using DiscManageMaster.Core.Classes;

namespace DiscManageMaster
{
    public partial class Loading : Form
    {
        public string Path = null;
        public string FolderName = null;
        public string Memo = null;
        public int IconIndex = -1;
        public int CollectionIndex = -1;

        public bool IsUpdate = false;
        public int DiscIndex = -1;

        public CommVar.FolderType FT;
        public ArrayList Plugins;
        public TreeNode TN;
        public Thread t;

        public Loading()
        {
            InitializeComponent();
            Core.Functions.OnFileAdded += OnFileAdded;
            Core.Functions.OnFolderAdded += OnFolderAdded;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            //if (CommVar.UseThread)
            //{
            //    t = new Thread(Run);
            //    t.IsBackground = true;
            //    t.Start();
            //}
            //else
                Run();
        }

        public void Run()
        {
            try
            {
                CDisc disc = new CDisc(new DirectoryInfo(Path), FolderName, IconIndex, Memo,
                                       Plugins.ToArray(typeof (CommVar.Plugins)) as CommVar.Plugins[], FT);
                TN = new TreeNode(disc.Name, IconIndex, IconIndex == CommVar.CloseFolderIconIndex ? CommVar.OpenFolderIconIndex : IconIndex);

                if (IsUpdate)
                {
                    PublicVar.root.Collection[CollectionIndex].RemoveAt(DiscIndex);
                    if (PublicVar.root.Collection[CollectionIndex].Count == DiscIndex)
                        PublicVar.root.Collection[CollectionIndex].Add(disc, TN.Nodes);
                    else
                        PublicVar.root.Collection[CollectionIndex].Add(disc, TN.Nodes, DiscIndex);
                }
                else
                    PublicVar.root.Collection[CollectionIndex].Add(disc, TN.Nodes);

                TN.Expand();
                DialogResult = DialogResult.OK;
            }
            catch
            {
                MessageBox.Show("Read file error! Operation aborted!", "Loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Abort;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            t.Abort();
            DialogResult = DialogResult.Cancel;
        }

        private void OnFileAdded(object sender, FileAddedEventArgs e)
        {
            label2.Text = e.File.Name;
        }

        private void OnFolderAdded(object sender, FolderAddedEventArgs e)
        {
            label2.Text = e.Folder.Name;
        }
    }
}

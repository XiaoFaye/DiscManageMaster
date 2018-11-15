using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using System.Windows.Forms;

namespace DiscManageMaster.Core.Classes
{
    public class SearchEngine
    {
        private SearchPosition SP;
        private string Keyword;
        private CRoot Root;
        private bool ThreadAbort = false;

        private System.Windows.Forms.Timer SearchTimer;
        private int SearchInterval = 0;

        public delegate void SearchResultItemAdd(object sender, SearchResultItemEventArgs e);
        public event SearchResultItemAdd OnSearchResultItemAdd;

        public delegate void SearchFinished(object sender, EventArgs e);
        public event SearchFinished OnSearchFinished;

        public SearchEngine(CRoot root, SearchPosition sp, string keyword)
        {
            Root = root;
            SP = sp;
            Keyword = keyword;
        }

        public void Reset(string keyword)
        {
            Keyword = keyword;
            ThreadAbort = false;
        }

        private void InitSearch()
        {
            if (SearchTimer != null) return;
            SearchTimer = new System.Windows.Forms.Timer();
            SearchTimer.Interval = 100;
            SearchTimer.Tick += SearchDelay;
            SearchTimer.Enabled = true;
        }

        private void SearchDelay(object sender, EventArgs e)
        {
            SearchInterval++;
            if (SearchInterval == 10)
            {
                SearchTimer.Enabled = false;
                SearchTimer = null;
                Start(false);
            }
        }

        public void Start(bool Restart)
        {
            InitSearch();
            if (Restart) SearchInterval = 0;
            if (SearchInterval < 10) return;
            if (CommVar.UseThread)
            {
                ThreadAbort = false;
                Thread t = new Thread(Search);
                t.IsBackground = true;
                t.Start();
            }
            else
                Search();
        }

        public void Stop()
        {
            ThreadAbort = true;
            SearchTimer.Enabled = false;
            SearchTimer = null;
            SearchInterval = 0;
        }

        private void Search()
        {
            try
            {
                string[] kws = GetKeywords(Keyword);

                switch (SP.ST)
                {
                    case SearchType.Root:
                        {
                            for (int i = 0; i < Root.Collection.Count; i++)
                            {
                                ArrayList temp1 = new ArrayList();
                                temp1.Add(i);
                                for (int j = 0; j < Root.Collection[i].Count; j++)
                                {
                                    ArrayList temp2 = (ArrayList)temp1.Clone();
                                    temp2.Add(j);
                                    OnSearchResultItemAdd(null, new SearchResultItemEventArgs(new SearchResultItem(MatchKeyword(kws, Root.Collection[i][j].Name.ToLower()), temp2, true)));
                                    SearchFolder(temp2, Root.Collection[i][j].Folders, Root.Collection[i][j].Files, kws);
                                }
                            }
                        }
                        break;
                    case SearchType.Collection:
                        {
                            int index = int.Parse(SP.Position[1].ToString());
                            for (int j = 0; j < Root.Collection[index].Count; j++)
                            {
                                ArrayList temp = (ArrayList)SP.Position.Clone();
                                temp.Add(j);
                                OnSearchResultItemAdd(null, new SearchResultItemEventArgs(new SearchResultItem(MatchKeyword(kws, Root.Collection[index][j].Name.ToLower()), temp, true)));
                                SearchFolder(temp, Root.Collection[index][j].Folders, Root.Collection[index][j].Files, kws);
                            }
                        }
                        break;
                    default:
                        {
                            int index1 = int.Parse(SP.Position[1].ToString());
                            int index2 = int.Parse(SP.Position[2].ToString());
                            ArrayList temp = new ArrayList();

                            for (int k = 0; k < SP.Position.Count; k++)
                                temp.Add(SP.Position[k]);

                            temp.RemoveAt(0);
                            temp.RemoveAt(0);
                            temp.RemoveAt(0);
                            CFolder folder = Root.Collection[index1][index2];
                            for (int i = 0; i < temp.Count; i++)
                            {
                                folder = folder.Folders[(int)temp[i]];
                            }

                            SearchFolder(SP.Position, folder.Folders, folder.Files, kws);
                        }
                        break;
                }

                OnSearchFinished(this, new EventArgs());
            }
            catch { }
        }

        private void SearchFolder(ArrayList searchposition, CFolderCollection folders, CFileCollection files, string[] keywords)
        {
            if (ThreadAbort) return;
            int i = -1;
            for (int j = 0; j < folders.Count; j++)
            {
                if (ThreadAbort) return;
                i++;
                searchposition.Add(i);
                OnSearchResultItemAdd(null, new SearchResultItemEventArgs(new SearchResultItem(MatchKeyword(keywords, folders[j].Name.ToLower()), searchposition, true)));
                SearchFolder(searchposition, folders[j].Folders, folders[j].Files, keywords);
                searchposition.RemoveAt(searchposition.Count - 1);
            }

            i = -1;
            for (int k = 0; k < files.Count; k++)
            {
                if (ThreadAbort) return;
                i++;
                searchposition.Add(i);
                OnSearchResultItemAdd(null, new SearchResultItemEventArgs(new SearchResultItem(MatchKeyword(keywords, files[k].Name.ToLower()), searchposition, false)));
                searchposition.RemoveAt(searchposition.Count - 1);
            }
        }

        private short MatchKeyword(string[] keywords, string content)
        {
            short matchtime = 0;
            for (int i = 0; i < keywords.Length; i++)
                if (content.IndexOf(keywords[i]) >= 0)
                    matchtime++;

            return matchtime;
        }

        private string[] GetKeywords(string keyword)
        {
            string kw = keyword.Trim().ToLower();
            while (kw.IndexOf("  ") >= 0)
                kw.Replace("  ", " ");

            return kw.Split(' ');
        }
    }

    public enum SearchType
    {
        Root = 1,
        Collection = 2,
        Folder = 3
    }

    public struct SearchPosition
    {
        public SearchType ST;
        public ArrayList Position;

        public SearchPosition(SearchType st, ArrayList pos)
        {
            ST = st;
            Position = pos;
        }
    }

    public class SearchResultItemEventArgs : EventArgs
    {
        public SearchResultItem Item;
        public SearchResultItemEventArgs(SearchResultItem item)
        {
            Item = item;
        }
    }

    public struct SearchResultItem : IComparable
    {
        public SearchResultItem(short matchtime, ArrayList item, bool isfolder)
        {
            MatchTime = matchtime;
            Item = item;
            IsFolder = isfolder;
        }

        public override string ToString()
        {
            string str = "";
            if (IsFolder)
                str = "1|";
            else
                str = "0|";

            for (int i = 0; i < Item.Count; i++)
                str += Item[i] + "|";

            return str.Substring(0, str.Length - 1);
        }

        public short MatchTime;
        public ArrayList Item;
        public bool IsFolder;

        public int CompareTo(object obj)
        {
            if (((SearchResultItem)obj).MatchTime == MatchTime)
                return 0;
            if (((SearchResultItem)obj).MatchTime > MatchTime)
                return -1;
            if (((SearchResultItem)obj).MatchTime < MatchTime)
                return 1;

            return 0;
        }
    }

    public class SearchHistory
    {
        public ListViewItem[] Historys;
        public string Location;
        public string Keyword;
    }
}

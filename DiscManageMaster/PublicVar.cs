using System;
using System.Collections.Generic;
using System.Text;
using DiscManageMaster.Core.Classes;
using System.Drawing;
using DiscManageMaster.Core;

namespace DiscManageMaster
{
    public static class PublicVar
    {
        public static CRoot root = CRoot.Load();
        public static CRoot backuproot = CRoot.Load();

        public static bool IsKeyValue(string Key, string UserName)
        {
            if (Key.Length != 37) return false;
            if (Key.ToUpper().IndexOf("DMS") == -1) return false;
            if (Key.Split('-').Length != 3) return false;

            string key1 = Properties.Resources.String1;
            string key2 = Properties.Resources.String2;
            string key3 = Controls.Properties.Resources.String3;
            string key4 = Core.Properties.Resources.String4;

            for (int a = 0; a < 16; a++)
                for (int b = 0; b < 16; b++)
                    for (int c = 0; c < 16; c++)
                        for (int d = 0; d < 16; d++)
                        {
                            string key = Functions.MD5(key1.Substring(a, 16) + key2.Substring(b, 16) + key3.Substring(c, 16) + key4.Substring(d, 16));

                            string str1 = "";
                            string str2 = "";
                            for (int i = 0; i < key.Length; i++)
                            {
                                if (char.IsNumber(key[i]))
                                    str1 += key[i].ToString();
                                else
                                    str2 += key[i].ToString();
                            }
                            string str = "dms-" + str1 + "-" + str2;
                            if (str.ToUpper() == Key.ToUpper())
                            {
                                DateTime dt = DateTime.Now;
                                RegData reg = new RegData(dt, a, b, c, d, key, UserName);
                                root.RegDate = dt;
                                root.UserName = UserName;
                                backuproot.RegDate = dt;
                                backuproot.UserName = UserName;
                                RegData.Save(reg);
                                CRoot.Save(root);

                                return true;
                            }
                        }
            return false;
        }

        public static bool IsRegisted()
        {
            try
            {
                RegData reg = RegData.Load();
                if (!reg.Right1(root.RegDate)) return false;
                if (!reg.GetName(root.UserName)) return false;

                string key1 = Properties.Resources.String1;
                string key2 = Properties.Resources.String2;
                string key3 = Controls.Properties.Resources.String3;
                string key4 = Core.Properties.Resources.String4;
                return reg.GetReg(key1, key2, key3, key4);
            }
            catch
            { }

            return false;
        }
    }

    public class Images
    {
        private static Image[] images = null;

        // ImageList.Images[int index] does not preserve alpha channel.
        static Images()
        {
            // TODO alpha channel PNG loader is not working on .NET Service RC1
            Bitmap bitmap = DiscManageMaster.Properties.Resources.ImageList24;
            int count = (int)(bitmap.Width / bitmap.Height);
            images = new Image[count];
            Rectangle rectangle = new Rectangle(0, 0, bitmap.Height, bitmap.Height);
            for (int i = 0; i < count; i++)
            {
                images[i] = bitmap.Clone(rectangle, bitmap.PixelFormat);
                rectangle.X += bitmap.Height;
            }
        }

        public static Image New
        {
            get { return images[0]; }
        }

        public static Image Open
        {
            get { return images[1]; }
        }

        public static Image Save
        {
            get { return images[2]; }
        }

        public static Image Cut
        {
            get { return images[3]; }
        }

        public static Image Copy
        {
            get { return images[4]; }
        }

        public static Image Paste
        {
            get { return images[5]; }
        }

        public static Image Delete
        {
            get { return images[6]; }
        }

        public static Image Properties
        {
            get { return images[7]; }
        }

        public static Image Undo
        {
            get { return images[8]; }
        }

        public static Image Redo
        {
            get { return images[9]; }
        }

        public static Image Preview
        {
            get { return images[10]; }
        }

        public static Image Print
        {
            get { return images[11]; }
        }

        public static Image Search
        {
            get { return images[12]; }
        }

        public static Image ReSearch
        {
            get { return images[13]; }
        }

        public static Image Help
        {
            get { return images[14]; }
        }

        public static Image ZoomIn
        {
            get { return images[15]; }
        }

        public static Image ZoomOut
        {
            get { return images[16]; }
        }

        public static Image Back
        {
            get { return images[17]; }
        }

        public static Image Forward
        {
            get { return images[18]; }
        }

        public static Image Favorites
        {
            get { return images[19]; }
        }

        public static Image AddToFavorites
        {
            get { return images[20]; }
        }

        public static Image Stop
        {
            get { return images[21]; }
        }

        public static Image Refresh
        {
            get { return images[22]; }
        }

        public static Image Home
        {
            get { return images[23]; }
        }

        public static Image Edit
        {
            get { return images[24]; }
        }

        public static Image Tools
        {
            get { return images[25]; }
        }

        public static Image Tiles
        {
            get { return images[26]; }
        }

        public static Image Icons
        {
            get { return images[27]; }
        }

        public static Image List
        {
            get { return images[28]; }
        }

        public static Image Details
        {
            get { return images[29]; }
        }

        public static Image Pane
        {
            get { return images[30]; }
        }

        public static Image Culture
        {
            get { return images[31]; }
        }

        public static Image Languages
        {
            get { return images[32]; }
        }

        public static Image History
        {
            get { return images[33]; }
        }

        public static Image Mail
        {
            get { return images[34]; }
        }

        public static Image Parent
        {
            get { return images[35]; }
        }

        public static Image FolderProperties
        {
            get { return images[36]; }
        }
    }
}

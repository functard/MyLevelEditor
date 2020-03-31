using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor
{
    class Map
    {
        public byte[,] MyMap
        {
            get
            {
                if (map != null)
                    return map;

                throw new NullReferenceException("Map is not created");
            }
        }

        private byte[,] map;

        #region Readonly Properties 
        public int Heightt
        {
            get
            { return map.GetLength(0); }
        }

        public int Widthh
        {
            get { return map.GetLength(1); }
        }

        #endregion


        #region Constructors
        public Map(int _widht, int _height)
        {
            map = new byte[_height, _widht];
        }

        public Map(string _path)
        {
            this.LoadMap(_path);
        }

        public Map(byte[,] _map)
        {
            map = _map;
        }
        #endregion



        public void SetElement(int x, int y, int value)
        {
            map[y, x] = (byte)value; //Todo check if valid x, y value
        }
        public int GetElement(int x, int y)
        {
            return Convert.ToInt32(map[y, x]); //Todo check if valid x, y value
        }

        public void ClearMap()
        {
            for (int i = 0; i < Heightt; i++)
            {
                for (int j = 0; j < Widthh; j++)
                {
                    map[i, j] = 0;
                }
            }
        }

        public void LoadMap(string path)
        {
            StreamReader reader = new StreamReader(path);

            int width = Convert.ToInt32(reader.ReadLine());
            int height = Convert.ToInt32(reader.ReadLine());

            byte[,] result = new byte[height, width];

            for (int i = 0; i < height; i++)
            {
                //read line by line
                var line = reader.ReadLine();
                //Split commas
                var split = line.Split(',');
                for (int j = 0; j < split.Length; j++)
                {
                    result[i, j] = (byte)Convert.ToInt32(split[j]);
                }

            }
            map = result;

            reader.Close();

        }

        public void SaveMap(string _path)
        {

            //Ready to write
            StreamWriter writer = new StreamWriter(_path);
            //Write dimension data
            writer.WriteLine(Widthh);
            writer.WriteLine(Heightt);

            for (int i = 0; i < Heightt; i++)
            {
                for (int j = 0; j < Widthh; j++)
                {
                    writer.Write(map[i, j]);
                    if (j < Widthh - 1)//no comma at the end of the line
                        writer.Write(",");
                }
                writer.WriteLine();
            }
            writer.Close();
        }

        //ToString
        public override string ToString()
        {
            string tmp = "";
            for (int i = 0; i < Heightt; i++)
            {
                for (int j = 0; j < Widthh; j++)
                {
                    tmp += map[i, j].ToString();
                }
                tmp += Environment.NewLine;
            }
            return tmp;
        }

    }
}


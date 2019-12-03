using LiteDatabase;
using SCPCB.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB
{
    public class GameInfo
    {
        ////////////////////
        //Static Variables//
        ////////////////////
        public static Dictionary<string, string> Language = new Dictionary<string, string>();
        public static Dictionary<string, CBInventoryItem> Items = new Dictionary<string, CBInventoryItem>();
        public static Database database = new Database(Application.persistentDataPath, DatabaseMode.OnDemand);
        public static GameInfo CurrentGame = new GameInfo();
        public static int TargetScene = 2;

        /////////////////////
        //In-Game Variables//
        /////////////////////
        public bool isPostProcessingEnabled = true;
        public bool isStickStatic = true;
        public bool isKeybroadAndMouseUsing = true;
        public bool isWarheadClosed = false;
        public Dictionary<string, Storage> Storages = new Dictionary<string, Storage>();
        public SCPMainCameraController mainCharacher;
    }

    [Serializable]

    public class Storage
    {
        public Vector2 Border=new Vector2(4,2);
        public Dictionary<Point2DD, string> items = new Dictionary<Point2DD, string>();
        public bool Put(CBInventoryItem item)
        {
            try
            {
                int i = items.Values.Count;
                Debug.Log(i);
                Point2DD point2DD = new Point2DD();
                point2DD.X = i % 4;
                point2DD.Y = i / 4;
                if (i % 4< Border.x && (i) / 4  < Border.y)
                {
                    items.Add(point2DD, item.ID);
                    return true;
                }

            }
            catch (Exception e)
            {
                Debug.Log(e);
                //throw e;
            }
            return false;
        }
    }

    [Serializable]
    public class Point2DF
    {
        public float X;
        public float Y;
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Point2DD))
            {
                return this == obj as Point2DF;
            }
            else if (obj.GetType() == typeof(Point2DF))
            {
                return this == obj as Point2DF;
            }
            else if (obj.GetType() == typeof(Vector2))
            {
                if (this.X == ((Vector2)obj).x)
                {
                    if (this.X == ((Vector2)obj).y)
                    {
                        return true;
                    }
                }
                return false;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"{X},{Y}";
        }
        public static bool operator ==(Point2DF A, Point2DF B)
        {
            if (A.ToString() == B.ToString())
            {
                return true;
            }
            else return false;
        }
        public static bool operator !=(Point2DF A, Point2DF B)
        {
            if (A.ToString() == B.ToString())
            {
                return false;
            }
            else return true;
        }
        public static implicit operator Point2DD(Point2DF d) => new Point2DD() { X = (int)d.X, Y = (int)d.Y };
    }
    [Serializable]
    public class Point2DD
    {
        public int X;
        public int Y;
        public override bool Equals(object obj)
        {
            if(obj.GetType()== typeof(Point2DD)){
                return this == obj as Point2DD;
            }
            else if(obj.GetType() == typeof(Point2DF)){
                return this == obj as Point2DD;
            }
            else if(obj.GetType() == typeof(Vector2)){
                if (this.X == ((Vector2)obj).x)
                {
                    if (this.X == ((Vector2)obj).y)
                    {
                        return true;
                    }
                }
                return false;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"{X},{Y}";
        }
        public static bool operator ==(Point2DD A, Point2DD B)
        {
            if (A.ToString() == B.ToString())
            {
                return true;
            }
            else return false;
        }
        public static bool operator !=(Point2DD A, Point2DD B)
        {
            if (A.ToString() == B.ToString())
            {
                return false;
            }
            else return true;
        }
        public static implicit operator Point2DF(Point2DD d) => new Point2DF() { X = d.X, Y = d.Y };
    }
}
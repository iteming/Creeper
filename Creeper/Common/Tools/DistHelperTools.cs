using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tools
{
    /// <summary>
    /// 距离
    /// </summary>
    public  class DistHelperTools
    {
        public static DistHelperTools _DistHelperTools = new DistHelperTools();

        /// <summary>
        /// 搜索半径 KM
        /// </summary>
        private const double EARTH_RADIUS = 6378.137;

        /// <summary>
        /// 求弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        /// <summary>
        /// 求角度
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private double angle(double rad ,double r)
        {
            return 180 * rad / (Math.PI * r);
        }

        private double g()
        {
            return 2 * Math.PI * EARTH_RADIUS;
        }

        /// <summary>
        /// 求距离
        /// </summary>
        /// <param name="Lat1"></param>
        /// <param name="Lng1"></param>
        /// <param name="Lat2"></param>
        /// <param name="Lng2"></param>
        /// <returns>//KM</returns>
        public string GetDistance(string Lat1str, string Lng1str, string Lat2str, string Lng2str)
        {
            double Lat1 = ConvertTools._ConvertTools.StringToDouble(Lat1str);
            double Lng1 = ConvertTools._ConvertTools.StringToDouble(Lng1str);
            double Lat2 = ConvertTools._ConvertTools.StringToDouble(Lat2str);
            double Lng2 = ConvertTools._ConvertTools.StringToDouble(Lng2str);
            double radLat1 = rad(Lat1);
            double radLat2 = rad(Lat2);
            double a = radLat1 - radLat2;
            double b = rad(Lng1) - rad(Lng2);
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s.ToString();
        }

        /// <summary>
        /// 求最近距离
        /// </summary>
        /// <param name="Lat1"></param>
        /// <param name="Lng1"></param>
        /// <param name="Lat2"></param>
        /// <param name="Lng2"></param>
        /// <returns>//KM</returns>
        public string GetDistance(string Lat1str, string Lng1str, List<string> LaLvList)
        {

            if (LaLvList.Count < 1)
                return "0";
            double Lat1;
            double Lng1;
            double Lat2;
            double Lng2;
            double radLat1;
            double radLat2;
            double a;
            double b;
            double s;
            List<double> list = new List<double>();
            foreach (string LaLv in LaLvList)
            {
                Lat1 = ConvertTools._ConvertTools.StringToDouble(Lat1str);
                Lng1 = ConvertTools._ConvertTools.StringToDouble(Lng1str);
                Lat2 = ConvertTools._ConvertTools.StringToDouble(LaLv.Substring(0, LaLv.IndexOf('_')));
                Lng2 = ConvertTools._ConvertTools.StringToDouble(LaLv.Substring(LaLv.IndexOf('_') + 1));
                radLat1 = rad(Lat1);
                radLat2 = rad(Lat2);
                a = radLat1 - radLat2;
                b = rad(Lng1) - rad(Lng2);
                s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
                Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
                s = s * EARTH_RADIUS;
                s = Math.Round(s * 10000) / 10000;
                list.Add(s);
            }
            return list.Select((m, index) => new { index, m }).OrderBy(n => n.m).Select(n=>n.m).FirstOrDefault().ToString();
        }

        /// <summary>
        /// 获取矩形两对角顶点坐标
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="rad"></param>
        /// <returns></returns>
        public List<SquarePoint> GetSquarePoint(double lat, double lng, double rad)
        {
            double lat1 = lat - angle(rad,EARTH_RADIUS);
            double lat2 = lat + angle(rad,EARTH_RADIUS);

            double r = Math.Abs(Math.Cos(lat)) * EARTH_RADIUS;
            double lng1 = lng - angle(rad, r);
            double lng2 = lng + angle(rad, r);

            return new List<SquarePoint>()
            {
                new SquarePoint(){ Lat=lat1,Lng=lng1},
                new SquarePoint(){ Lat=lat2,Lng=lng2}
            };
        }
    }

    public class SquarePoint
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double Lat { set; get; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Lng { set; get; }
    }
}

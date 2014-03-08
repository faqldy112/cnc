using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
namespace Laser_Engraver
{
    public partial class frm_img : Form
    {
        Bitmap m_RulerX;
        Bitmap m_RulerY;
        Graphics g;
        float m_dpiX;
        float m_dpiY;
        Pen m_rulerPen;
        string persp;
        Image m_image;
        Image origin_image;
        Image unzoom_image;
        public Form caller;
        private static PixelFormat[] indexedPixelFormats = { PixelFormat.Undefined, PixelFormat.DontCare,
PixelFormat.Format16bppArgb1555, PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed,
PixelFormat.Format8bppIndexed
    };
        public delegate void UpdateGUIThreadDelegate(int i);
        UpdateGUIThreadDelegate UpdateGUIAction;
        public frm_img(Image img)
        {
            InitializeComponent();
            g = this.CreateGraphics();
            m_dpiX = g.DpiX;
            m_dpiY = g.DpiY;
            m_rulerPen = new Pen(Color.Black, 1f);
            m_image = img;
            origin_image =(Image) img.Clone();
            unzoom_image = (Image)img.Clone();
            pic_image.Image = img;
            persp = "pixel";
            initRuler(1f);
            UpdateGUIAction = new UpdateGUIThreadDelegate(UpdateProgress);
        }
        private void initRuler(float zoom)
        {
            m_RulerX = new Bitmap(500, 20);
            g = Graphics.FromImage(m_RulerX);
            g.FillRectangle(Brushes.White, 0f, 0f, 500f, 20f);
            g.DrawLine(m_rulerPen, new PointF(0f, 19f), new PointF(499f, 19f));
            g.DrawLine(m_rulerPen, new PointF(0f, 0f), new PointF(0f, 20f));
            if (persp == "realeye")
            {
                //g.DrawString("0mm"
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (j % 5 != 0)
                        {
                            g.DrawLine(m_rulerPen, new PointF(MilToPixelX(j + (i * 10), zoom) * zoom, 19), new PointF(MilToPixelX(j + (i * 10), zoom) * zoom, 15));
                        }
                        else
                        {
                            g.DrawLine(m_rulerPen, new PointF(MilToPixelX(j + (i * 10), zoom) * zoom, 19), new PointF(MilToPixelX(j + (i * 10), zoom) * zoom, 10));
                        }
                    }
                    if ((i / zoom) % 1 == 0)
                        g.DrawString((i / zoom).ToString(), new Font("System", 7f, FontStyle.Regular), Brushes.Black, new PointF(MilToPixelX(i * 10, zoom) + 1, 3));
                    g.DrawLine(m_rulerPen, new PointF(MilToPixelX(i * 10, zoom) * zoom, 0), new PointF(MilToPixelX(i * 10, zoom) * zoom, 20));
                }
            }
            else if (persp == "pixel")
            {
                
                for (int i = 5; i < 500; i+=5)
                {
                    if (i % 10 == 0&&i%50!=0)
                    {
                        g.DrawLine(m_rulerPen,new PointF(i*zoom,19),new PointF(i*zoom,10));
                    }
                    else if (i % 50 == 0)
                    {
                        g.DrawLine(m_rulerPen, new PointF(i * zoom, 19), new PointF(i * zoom, 0));
                        if(i/zoom%10==0)
                        g.DrawString((i / zoom).ToString(), new Font("System", 7f, FontStyle.Regular), Brushes.Black, new PointF( i+ 1, 1));
                    }
                    else
                    {
                        g.DrawLine(m_rulerPen, new PointF(i * zoom, 19), new PointF(i * zoom, 15));
                    }
                }
            }
            else if (persp == "millimeter")
            {
                for (int i = 10; i < 500; i +=10)
                {
                    if (i % 10 == 0 && i % 50 != 0)
                    {
                        g.DrawLine(m_rulerPen, new PointF(i * zoom, 19), new PointF(i * zoom, 10));
                    }
                    else if (i % 50 == 0)
                    {
                        g.DrawLine(m_rulerPen, new PointF(i * zoom, 19), new PointF(i * zoom, 0));
                        if (i / zoom % 10 == 0)
                            g.DrawString((i / zoom/10).ToString(), new Font("System", 7f, FontStyle.Regular), Brushes.Black, new PointF(i + 1, 1));
                    }
                    else
                    {
                        g.DrawLine(m_rulerPen, new PointF(i * zoom, 19), new PointF(i * zoom, 15));
                    }
                }
            }
            pic_rulerX.Image = m_RulerX;
            /////Y轴

            m_RulerY = new Bitmap(20, 500);
            g = Graphics.FromImage(m_RulerY);
            g.FillRectangle(Brushes.White, 0, 0, 500, 20);
            g.DrawLine(m_rulerPen, new PointF(19, 0), new PointF(19, 499));
            g.DrawLine(m_rulerPen, new PointF(0, 0), new PointF(20, 0));
            if (persp == "realeye")
            {
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (j % 5 != 0)
                        {
                            g.DrawLine(m_rulerPen, new PointF(19, MilToPixelY(j + (i * 10), zoom) * zoom), new PointF(15, MilToPixelY(j + (i * 10), zoom) * zoom));
                        }
                        else
                        {
                            g.DrawLine(m_rulerPen, new PointF(19, MilToPixelY(j + (i * 10), zoom) * zoom), new PointF(10, MilToPixelY(j + (i * 10), zoom) * zoom));
                        }
                    }
                    if ((i / zoom) % 1 == 0)
                        g.DrawString((i / zoom).ToString(), new Font("System", 7f, FontStyle.Regular), Brushes.Black, new PointF(3, MilToPixelY(i * 10, zoom) + 1));
                    g.DrawLine(m_rulerPen, new PointF(0, MilToPixelY(i * 10, zoom) * zoom), new PointF(20, MilToPixelX(i * 10, zoom) * zoom));
                }
            }
            else if (persp == "pixel")
            {
                for (int i = 5; i < 500; i += 5)
                {
                    if (i % 10 == 0 && i % 50 != 0)
                    {
                        g.DrawLine(m_rulerPen, new PointF( 19,i * zoom), new PointF(10,i * zoom ));
                    }
                    else if (i % 50 == 0)
                    {
                        g.DrawLine(m_rulerPen, new PointF(19,i * zoom ), new PointF(0,i * zoom ));
                        if (i / zoom % 10 == 0)
                            g.DrawString((i / zoom).ToString(), new Font("System", 7f, FontStyle.Regular), Brushes.Black, new PointF(1,i + 1));
                    }
                    else
                    {
                        g.DrawLine(m_rulerPen, new PointF(19,i * zoom ), new PointF(15,i * zoom ));
                    }
                }
            }
            else if (persp == "millimeter")
            {
                for (int i = 10; i < 500; i += 10)
                {
                    if (i % 10 == 0 && i % 50 != 0)
                    {
                        g.DrawLine(m_rulerPen, new PointF(19, i * zoom), new PointF(10, i * zoom));
                    }
                    else if (i % 50 == 0)
                    {
                        g.DrawLine(m_rulerPen, new PointF(19, i * zoom), new PointF(0, i * zoom));
                        if (i / zoom % 10 == 0)
                            g.DrawString((i / zoom/10).ToString(), new Font("System", 7f, FontStyle.Regular), Brushes.Black, new PointF(1, i + 1));
                    }
                    else
                    {
                        g.DrawLine(m_rulerPen, new PointF(19, i * zoom), new PointF(15, i * zoom));
                    }
                }
            }
            pic_rulerY.Image = m_RulerY;





        }
        private float MilToPixelX(float milli, float zoom)
        {
            return m_dpiX * milli / 25.4f;
        }
        private float MilToPixelY(float milli, float zoom)
        {
            return m_dpiY * milli / 25.4f;
        }
        private void UpdateProgress(int i)
        {
        }
        
        private void track_zoom_Scroll(object sender, EventArgs e)
        {
            initRuler((float)track_zoom.Value);
            Image temp_image = m_image;
            Bitmap bmp = (Bitmap)temp_image;
            int w;
            int h;
            if (persp == "realeye")
            {
                w = (int)Math.Truncate((double)MilToPixelX(temp_image.Width * track_zoom.Value / 10, track_zoom.Value));
                h = (int)Math.Truncate((double)MilToPixelX(temp_image.Height * track_zoom.Value / 10, track_zoom.Value));
            }
            else
            {
                 w = temp_image.Width * track_zoom.Value;
                h = temp_image.Height * track_zoom.Value;
            }
          
            Bitmap b = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(b); // 插值算法的质量 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(bmp, new Rectangle(0, 0, w, h), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
            g.Dispose();
            pic_image.Image = b;
        }

        private void rb_pixel_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_pixel.Checked)
            {
                 persp = "pixel";
                 initRuler((float)track_zoom.Value);
                 Image temp_image = m_image;
                 Bitmap bmp = (Bitmap)temp_image;
                 int w;
                 int h;
                 w = temp_image.Width * track_zoom.Value;
                 h = temp_image.Height * track_zoom.Value;

                 Bitmap b = new Bitmap(w, h);
                 Graphics g = Graphics.FromImage(b); // 插值算法的质量 
                 g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                 g.DrawImage(bmp, new Rectangle(0, 0, w, h), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                 g.Dispose();
                 pic_image.Image = b;
            }
        }

        private void rb_realeye_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_realeye.Checked)
            {
                persp = "realeye";
                initRuler((float)track_zoom.Value);
                Image temp_image = m_image;
                Bitmap bmp = (Bitmap)temp_image;
                int w;
                int h;
               
                    w = (int)Math.Truncate((double)MilToPixelX(temp_image.Width * track_zoom.Value / 10, track_zoom.Value));
                    h = (int)Math.Truncate((double)MilToPixelX(temp_image.Height * track_zoom.Value / 10, track_zoom.Value));
              

                Bitmap b = new Bitmap(w, h);
                Graphics g = Graphics.FromImage(b); // 插值算法的质量 
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, w, h), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                pic_image.Image = b;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_millimeter.Checked)
            {
                persp = "millimeter";
                initRuler((float)track_zoom.Value);
                Image temp_image = m_image;
                Bitmap bmp = (Bitmap)temp_image;
                int w;
                int h;
                w = temp_image.Width * track_zoom.Value;
                h = temp_image.Height * track_zoom.Value;
              


                Bitmap b = new Bitmap(w, h);
                Graphics g = Graphics.FromImage(b); // 插值算法的质量 
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, w, h), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                pic_image.Image = b;
            }
        }

        private void btn_gray_Click(object sender, EventArgs e)
        {
            pic_image.Image = ToGray((Bitmap)pic_image.Image);
            unzoom_image = ToGray((Bitmap)unzoom_image);
        }

        #region 图片处理

        /// <summary>
        /// 图片灰度处理
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Bitmap ToGray(Bitmap bmp)
        {
            bmp = ConvertPixelFormat(bmp);
                for (int i = 0; i < bmp.Width; i++)
                {
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        //获取该点的像素的RGB的颜色
                        Color color = bmp.GetPixel(i, j);
                        //利用公式计算灰度值
                        int gray = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                        Color newColor = Color.FromArgb(gray, gray, gray);
                        bmp.SetPixel(i, j, newColor);
                    }
                }
                return bmp;
          
        }
        /// <summary>
        /// Convert PixelFormat
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        private static Bitmap ConvertPixelFormat(Bitmap bmp)
        {
            if (IsPixelFormatIndexed(bmp.PixelFormat))
            {
                Bitmap t_bmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);
                t_bmp.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
                using (Graphics g = Graphics.FromImage(t_bmp))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.DrawImage(bmp, 0, 0);
                    
                }
                return t_bmp;
            }
            else
                return bmp;
            
        }
        /// <summary>
        /// 反向处理
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Bitmap GrayReverse(Bitmap bmp)
        {

            bmp = ConvertPixelFormat(bmp);
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //获取该点的像素的RGB的颜色
                    Color color = bmp.GetPixel(i, j);
                    Color newColor = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            return bmp;
        }
        /// <summary>
        /// 图像二值化1：取图片的平均灰度作为阈值，低于该值的全都为0，高于该值的全都为255
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Bitmap ConvertTo1Bpp1(Bitmap bmp)
        {
            bmp = ConvertPixelFormat(bmp);
            int average = 0;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    average += color.B;
                }
            }
            average = (int)average / (bmp.Width * bmp.Height);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //获取该点的像素的RGB的颜色
                    Color color = bmp.GetPixel(i, j);
                    int value = 255 - color.B;
                    Color newColor = value > average ? Color.FromArgb(0, 0, 0) : Color.FromArgb(255,

255, 255);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            return bmp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgPixelFormat"></param>
        /// <returns></returns>
        private static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            foreach (PixelFormat pf in indexedPixelFormats)
            {
                if (pf.Equals(imgPixelFormat)) return true;
            }

            return false;
        }
        /// <summary>
        /// 水平翻转
        /// </summary>
        /// <param name="mybm"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Bitmap RevPic(Bitmap mybm)
        {
            int width = mybm.Width;
            int height = mybm.Height;
            Bitmap bm = new Bitmap(width, height);//初始化一个记录经过处理后的图片对象
            int x, y, z;//x,y是循环次数,z是用来记录像素点的x坐标的变化的
            Color pixel;



            for (y = height - 1; y >= 0; y--)
            {
                for (x = width - 1, z = 0; x >= 0; x--)
                {
                    pixel = mybm.GetPixel(x, y);//获取当前像素的值
                    bm.SetPixel(z++, y, Color.FromArgb(pixel.R, pixel.G, pixel.B));//绘图
                }
            }

 



            return bm;//返回翻转后的图片
        }
        #endregion

        private void btn_reserve_Click(object sender, EventArgs e)
        {
            pic_image.Image = GrayReverse((Bitmap)pic_image.Image);
            unzoom_image = GrayReverse((Bitmap)unzoom_image);
        }

        private void btn_lppo_Click(object sender, EventArgs e)
        {
            pic_image.Image = ConvertTo1Bpp1((Bitmap)pic_image.Image);
            unzoom_image = ConvertTo1Bpp1((Bitmap)unzoom_image);
        }

        private void btn_mirror_Click(object sender, EventArgs e)
        {
            pic_image.Image = this.RevPic((Bitmap)pic_image.Image);
            unzoom_image = RevPic((Bitmap)unzoom_image);
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            pic_image.Image = origin_image;
        }

        private void btn_engrave_Click(object sender, EventArgs e)
        {
            ((frm_main)caller).SetTargetImage((Bitmap)pic_image.Image);

            List<String> gcode = new List<string>();
            gcode.Add("G00 X0.000 Y0.000\n");
            int bmpx = unzoom_image.Width;
            int bmpy = unzoom_image.Height;
            int pixelCount = bmpx * bmpy;
            float curr_x = 0;
            float curr_y = 0;
            for (int y = 0; y < bmpy; y++)
            {
                int blank = 0;
                bool blankdisappear=false;
                for (int x = 0; x < bmpx; x++)
                {
                    if (blank > 0 && blankdisappear)
                    {
                        gcode.Add(string.Format("G00 X{0} y{1}",curr_x+(0.1*blank),curr_y));
                    }
                    
                }
                curr_y = curr_y + 0.1f;
            }

            this.Close();
        }
        
    }
}

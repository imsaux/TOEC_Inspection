using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using TOEC_SASTAD_Data.Model;
using TOEC_SASTAD_Data.BLL;
using System.Drawing.Imaging;
using System.Net;
using TOEC_Common;

namespace TOEC_Inspection
{
    public partial class frm_ViewTrainDetail : DevExpress.XtraEditors.XtraForm
    {
        #region 初始化
        public frm_ViewTrainDetail()
        {
            InitializeComponent();
        }
        public View_TrainDetail CurTrainDetail { get; set; }
        public string PicPath_L { get; set; }//左图
        public string PicPath_R { get; set; }//右图
        public string PicPath_ZL { get; set; }//走行部左图
        public string PicPath_ZR { get; set; }//走行部右图
        public List<View_TrainDetail> ProblemTrainDetails { get; set; }//当前列车所有问题车厢顺位号
        private List<alarmdetail> list_ad = new List<alarmdetail>();//当前车厢所有问题

        private void frm_Pic_Load(object sender, EventArgs e)
        {
            pictureEdit_Main.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            Init_AllProblemTrainDetailOfCurTrain();
            Init_Load_TrainDetailInfo();
        }

        /// <summary>
        /// 获去所有问题车厢
        /// </summary>
        private void Init_AllProblemTrainDetailOfCurTrain()
        {
            BLL_Car tdbll = new BLL_Car();
            if (ProblemTrainDetails == null)
                ProblemTrainDetails = tdbll.GetAllProblemCar(CurTrainDetail.Train_ID.ToString(), CurTrainDetail.TrainComeDate);
        }
        /// <summary>
        /// 初始化当前车厢信息和图片
        /// </summary>
        private void Init_Load_TrainDetailInfo()
        {
            try
            {
                if (CurTrainDetail == null && ProblemTrainDetails != null && ProblemTrainDetails.Count > 0)
                {
                    CurTrainDetail = ProblemTrainDetails[0];
                }
                string FolderName = new BLL_Line().GetLineFolderName(CurTrainDetail.LineID);
                string ServerIP = Config.DB_IP;
                PicPath_L = @"\\" + ServerIP + @"\GQPics\" + FolderName + @"\" + CurTrainDetail.TrainComeDate.ToString("yyyyMMddHHmmss") + @"\L" + CurTrainDetail.TrainDetail_OrderNo.ToString().PadLeft(3, '0') + "_" + CurTrainDetail.TrainDetail_OrderNo + ".jpg";
                PicPath_R = @"\\" + ServerIP + @"\GQPics\" + FolderName + @"\" + CurTrainDetail.TrainComeDate.ToString("yyyyMMddHHmmss") + @"\R" + CurTrainDetail.TrainDetail_OrderNo.ToString().PadLeft(3, '0') + "_" + CurTrainDetail.TrainDetail_OrderNo + ".jpg";
                PicPath_ZL = @"\\" + ServerIP + @"\ZXGQPics\" + FolderName + @"\" + CurTrainDetail.TrainComeDate.ToString("yyyyMMddHHmmss") + @"\ZL" + CurTrainDetail.TrainDetail_OrderNo.ToString().PadLeft(3, '0') + "_" + CurTrainDetail.TrainDetail_OrderNo + ".jpg";
                PicPath_ZR = @"\\" + ServerIP + @"\ZXGQPics\" + FolderName + @"\" + CurTrainDetail.TrainComeDate.ToString("yyyyMMddHHmmss") + @"\ZR" + CurTrainDetail.TrainDetail_OrderNo.ToString().PadLeft(3, '0') + "_" + CurTrainDetail.TrainDetail_OrderNo + ".jpg";
                barStaticItem_CurInfo.Caption = "线路：" + CurTrainDetail.LineID
                    + " 过车时间：" + CurTrainDetail.TrainComeDate
                    + " 车次：" + CurTrainDetail.TrainNo
                    + " 车号：" + CurTrainDetail.TrainDetail_No
                    + " 车型：" + CurTrainDetail.vehicletype
                    + " 顺位：" + CurTrainDetail.TrainDetail_OrderNo
                    + " ";
                //加载当前车厢所有报警,优先展示有报警一侧图片
                BLL_AlarmDetail adbll = new BLL_AlarmDetail();
                list_ad = adbll.GetAlarmByCarID(CurTrainDetail.TrainDetail_ID.ToString());
                if (list_ad != null && list_ad.Count > 0 && list_ad[0].Side != "")
                {
                    LoadPic(list_ad[0].Side);
                    btn_Pic_L.Caption = "左图" + list_ad.FindAll(n => n.Side == "L").Count;
                    btn_Pic_R.Caption = "右图" + list_ad.FindAll(n => n.Side == "R").Count;
                    btn_ZXPic_L.Caption = "走行部左" + list_ad.FindAll(n => n.Side == "ZL").Count;
                    btn_ZXPic_R.Caption = "走行部右" + list_ad.FindAll(n => n.Side == "ZR").Count;
                    lbl_AlarmCount.Caption = "本节车共计报警：" + list_ad.Count;
                }
                else
                {
                    LoadPic("L");
                    btn_Pic_L.Caption = "左图";
                    btn_Pic_R.Caption = "右图";
                    btn_ZXPic_L.Caption = "走行部左";
                    btn_ZXPic_R.Caption = "走行部右";
                    lbl_AlarmCount.Caption = "本列车共计报警：0";
                }
            }
            catch (Exception ex) { XtraMessageBox.Show("初始化失败：" + ex.Message); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftpUrl">FTP地址</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public Stream Info(string ftpUrl, string fileName)
        {
            try
            {
                FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpUrl + "" + fileName));
                reqFtp.UseBinary = true;
                FtpWebResponse respFtp = (FtpWebResponse)reqFtp.GetResponse();
                Stream stream = respFtp.GetResponseStream();
                return stream;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 图像信息

        #region 图像切换按钮：左图、右图、走行部左等
        /// <summary>
        /// 左图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pic_L_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadPic("L");
        }
        /// <summary>
        /// 右图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pic_R_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadPic("R");
        }
        /// <summary>
        /// 走行部左图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ZXPic_L_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadPic("ZL");
        }
        /// <summary>
        /// 走行部右图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ZXPic_R_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadPic("ZR");
        }
        #endregion

        #region 加载图片和报警框位置
        /// <summary>
        /// 加载主图片
        /// </summary>
        public void LoadPic(string Side)
        {
            try
            {
                pictureEdit_Main.Dock = DockStyle.Fill;
                Bitmap SourceImage = null;
                switch (Side)
                {
                    case "L":
                        if (File.Exists(PicPath_L))
                            SourceImage = new Bitmap(PicPath_L);
                        else
                            throw new Exception("图像不存在或未共享");
                        break;
                    case "R":
                        if (File.Exists(PicPath_R))
                            SourceImage = new Bitmap(PicPath_R);
                        else
                            throw new Exception("图像不存在或未共享");
                        break;
                    case "ZL":
                        if (File.Exists(PicPath_ZL))
                            SourceImage = new Bitmap(PicPath_ZL);
                        else
                            throw new Exception("图像不存在或未共享");
                        break;
                    case "ZR":
                        if (File.Exists(PicPath_ZR))
                            SourceImage = new Bitmap(PicPath_ZR);
                        else
                            throw new Exception("图像不存在或未共享");
                        break;
                    default: return;
                }
                int Ratio = 10;//压缩比率
                //即使不压缩，也必须用bitmap在转换一次
                Bitmap tmp = new Bitmap(SourceImage.Width / Ratio, SourceImage.Height / Ratio, PixelFormat.Format32bppArgb);

                //画框
                using (Graphics g = Graphics.FromImage(tmp))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;//HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;//HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;//HighQuality;
                    g.DrawImage(SourceImage, new Rectangle(0, 0, SourceImage.Width / Ratio, SourceImage.Height / Ratio), new Rectangle(0, 0, SourceImage.Width, SourceImage.Height), GraphicsUnit.Pixel);
                    foreach (alarmdetail ad in list_ad)
                    {
                        if (ad.Side == Side)
                        {
                            //画框
                            string[] AlarmRect = ad.FileContent.Split(',');
                            if (AlarmRect.Length != 4) { continue; }
                            Rectangle rect = new Rectangle(
                                Convert.ToInt32(AlarmRect[0]) / Ratio,
                                Convert.ToInt32(AlarmRect[1]) / Ratio,
                                Convert.ToInt32(AlarmRect[2]) / Ratio,
                                Convert.ToInt32(AlarmRect[3]) / Ratio);
                            g.DrawRectangle(new Pen(Color.Red), rect);
                            //写报警文字信息
                            PointF pointf = new PointF();
                            if (rect.X < 500)
                            { pointf = new PointF(rect.X + 5, rect.Y + rect.Height + 5); }
                            else if ((SourceImage.Width / Ratio - rect.X - rect.Width) < 80)
                            {
                                //根据字数往左边移
                                pointf = new PointF(rect.X - (ad.ProblemType.Length + 4) * 6, rect.Y + rect.Height + 5);
                            }
                            else { pointf = new PointF(rect.X, rect.Y + rect.Height + 5); }
                            if (rect.Y + rect.Height >= SourceImage.Height / Ratio - 20)
                            {
                                pointf = new PointF(pointf.X, rect.Y - 25);
                            }
                            g.DrawString(ad.ProblemType + " " + ad.AlarmLevel + "级", new Font("微软雅黑", 14), new SolidBrush(Color.Red), pointf);
                            SourceImage = tmp;
                        }
                        else { continue; }
                    }
                    pictureEdit_Main.Image = SourceImage;
                    barStaticItem_PictureState.Caption = (Side.Contains("L") ? "左" : "右") + "侧 ";
                }
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            finally
            {
                GC.Collect();
            }
        }
        #endregion

        #region 图像放大、缩小、拖拽
        /// <summary>
        /// 图片滚轮以鼠标中心放大缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureEdit_Main_Properties_MouseWheel(object sender, MouseEventArgs e)
        {
            double step = 1.2;//缩放倍率
            if (e.Delta > 0)//以鼠标为中心放大
            {
                //放大则解锁拖拽
                pictureEdit_Main.Dock = DockStyle.None;
                //放大
                pictureEdit_Main.Height = (int)(pictureEdit_Main.Height * step);
                pictureEdit_Main.Width = (int)(pictureEdit_Main.Width * step);
                int px_add = (int)(e.X * (step - 1.0));
                int py_add = (int)(e.Y * (step - 1.0));
                pictureEdit_Main.Location = new Point(pictureEdit_Main.Location.X - px_add, pictureEdit_Main.Location.Y - py_add);
            }
            else
            {
                //缩小
                pictureEdit_Main.Height = (int)(pictureEdit_Main.Height / step);
                pictureEdit_Main.Width = (int)(pictureEdit_Main.Width / step);
                int px_add = (int)(e.X * (1.0 - 1.0 / step));
                int py_add = (int)(e.Y * (1.0 - 1.0 / step));
                pictureEdit_Main.Location = new Point(pictureEdit_Main.Location.X + px_add, pictureEdit_Main.Location.Y + py_add);
            }

            //最小不得小于容器，锁定拖拽
            if (pictureEdit_Main.Width <= np_Pic.Width || pictureEdit_Main.Height <= np_Pic.Height)
            {
                pictureEdit_Main.Dock = DockStyle.Fill;
            }

        }

        /// <summary>
        /// 拖拽
        /// </summary>
        private void pictureEdit_Main_Properties_MouseMove(object sender, MouseEventArgs e)
        {
            pictureEdit_Main.Focus();
            //拖拽
            if (e.Button == MouseButtons.Left && pictureEdit_Main.Dock == DockStyle.None)
            {
                pictureEdit_Main.Left += e.X - MouseMoveStart.X;
                pictureEdit_Main.Top += e.Y - MouseMoveStart.Y;
            }
        }
        //记录鼠标拖动的开始位置
        private Point MouseMoveStart = Point.Empty;
        private void pictureEdit_Main_Properties_MouseDown(object sender, MouseEventArgs e)
        {
            MouseMoveStart = e.Location;
        }
        #endregion 

        #endregion

        #region 热轮信息

        #endregion

        #region 车厢切换
        /// <summary>
        /// 上一节车厢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Previous_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CurTrainDetail.TrainDetail_OrderNo == 1)
            {
                XtraMessageBox.Show("没有上一节了");
                return;
            }
            else
            {
                BLL_Car tdbll = new BLL_Car();
                View_TrainDetail tmp = tdbll.GetCar(CurTrainDetail.Train_ID.ToString(), CurTrainDetail.TrainComeDate, Convert.ToInt16(CurTrainDetail.TrainDetail_OrderNo - 1));
                if (tmp != null)
                {
                    CurTrainDetail = tmp;
                    Init_Load_TrainDetailInfo();
                }
            }
        }
        /// <summary>
        /// 下一节车厢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Next_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BLL_Car tdbll = new BLL_Car();
            View_TrainDetail tmp = tdbll.GetCar(CurTrainDetail.Train_ID.ToString(), CurTrainDetail.TrainComeDate, Convert.ToInt16(CurTrainDetail.TrainDetail_OrderNo + 1));
            if (tmp != null)
            {
                CurTrainDetail = tmp;
                Init_Load_TrainDetailInfo();
            }
            else { XtraMessageBox.Show("没有下一节了"); }
        }

        /// <summary>
        /// 上一节问题车厢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Previous_Problem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ProblemTrainDetails != null && ProblemTrainDetails.Count > 0)
            {
                int ABS_min = 9999;//最小绝对值
                int ABS_min_i = 0;//最小绝对值对应的序号
                for (int i = 0; i < ProblemTrainDetails.Count; i++)
                {
                    int tmpABS = Math.Abs(Convert.ToInt16(ProblemTrainDetails[i].TrainDetail_OrderNo) - Convert.ToInt16(CurTrainDetail.TrainDetail_OrderNo));
                    if (tmpABS < ABS_min)
                    {
                        ABS_min = tmpABS;
                        ABS_min_i = i;
                    }
                }
                if (Convert.ToInt16(ProblemTrainDetails[ABS_min_i].TrainDetail_OrderNo) >= CurTrainDetail.TrainDetail_OrderNo)
                {
                    if (ABS_min_i - 1 >= 0)
                        CurTrainDetail = ProblemTrainDetails[ABS_min_i - 1];
                    else XtraMessageBox.Show("前面没有问题车了");
                }
                else
                {
                    CurTrainDetail = ProblemTrainDetails[ABS_min_i];
                }
                Init_Load_TrainDetailInfo();
            }
            else XtraMessageBox.Show("本列车均无问题");
        }
        /// <summary>
        /// 下一节问题车厢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Next_Problem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ProblemTrainDetails != null && ProblemTrainDetails.Count > 0)
            {
                int ABS_min = 9999;//最小绝对值
                int ABS_min_i = 0;//最小绝对值对应的序号
                for (int i = 0; i < ProblemTrainDetails.Count; i++)
                {
                    int tmpABS = Math.Abs(Convert.ToInt16(ProblemTrainDetails[i].TrainDetail_OrderNo) - Convert.ToInt16(CurTrainDetail.TrainDetail_OrderNo));
                    if (tmpABS < ABS_min)
                    {
                        ABS_min = tmpABS;
                        ABS_min_i = i;
                    }
                }
                if (Convert.ToInt16(ProblemTrainDetails[ABS_min_i].TrainDetail_OrderNo) <= CurTrainDetail.TrainDetail_OrderNo)
                {
                    if (ABS_min_i + 1 < ProblemTrainDetails.Count)
                        CurTrainDetail = ProblemTrainDetails[ABS_min_i + 1];
                    else XtraMessageBox.Show("后面没有问题车了");
                }
                else
                {
                    CurTrainDetail = ProblemTrainDetails[ABS_min_i];
                }
                Init_Load_TrainDetailInfo();
            }
            else XtraMessageBox.Show("本列车均无问题");
        }
        #endregion
    }
}
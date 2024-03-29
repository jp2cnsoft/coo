using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using Seika.COO.DBA.MA;
using Seika.COO.Web.PG;
using Seika.Db;
using Seika.COO.DBA.BS;
using Seika.COO.DBA.SV;
using Seika.Api;

namespace Seika.COO.PageData
{
    /// <summary>
    /// 问卷调查
    /// </summary>
    /// <remarks>
    /// 2008/05/13  杨李
    /// </remarks>
    public class P3000P9020 : PageDataBase
    {
        //String qname = String.Empty;
        //String answer = String.Empty;
        //String answer_1 = String.Empty;
        String username = String.Empty;
        String usertel = String.Empty;
        String userpost = String.Empty;
        String useremail = String.Empty;
        //String qnubmer = String.Empty;


        //static Hashtable dts = new Hashtable();
        string ma_guestid = "";

        DBConnect conn = null;

        public P3000P9020()
            : base()
        {
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 数据清空
        /// </summary>
        public override void Remove()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 取得数据
        /// </summary>
        public override void Load()
        {
            //传递提交信息
            MA_GUEST ma_guest = new MA_GUEST(conn);
            ma_guestid = ma_guest.GetNextSeqNumber();
            if (!(new MA_GUEST(conn)).Adduser(ma_guestid, username, userpost, usertel, useremail))
            {
                throw new System.Exception("ED01000380");
            }
        }

        public void Open()
        {
            conn = this.GetDbConnect();
        }

        public void Close()
        {
            conn.Commit();
            conn.close();
        }

        public void Update(Question q1,Question q21,Question q22,Question q23,Question q3,Question q4)
        {

            //传递提交信息
            DE_USQUESTION de_usquestion = new DE_USQUESTION(conn);
            string q1id = de_usquestion.GetNextSeqNumber() + q1.Qnumber;
            if (!(new DE_USQUESTION(conn)).Addquestion(q1id, ma_guestid, q1.Qname, q1.Answer, q1.Answer_1))
            {
                throw new System.Exception("ED01000380");
            }
            string q21id = de_usquestion.GetNextSeqNumber() + q21.Qnumber;
            if (!(new DE_USQUESTION(conn)).Addquestion(q21id, ma_guestid, q21.Qname, q21.Answer, q21.Answer_1))
            {
                throw new System.Exception("ED01000380");
            }
            string q22id = de_usquestion.GetNextSeqNumber() + q22.Qnumber;
            if (!(new DE_USQUESTION(conn)).Addquestion(q22id, ma_guestid, q22.Qname, q22.Answer, q22.Answer_1))
            {
                throw new System.Exception("ED01000380");
            }
            string q23id = de_usquestion.GetNextSeqNumber() + q23.Qnumber;
            if (!(new DE_USQUESTION(conn)).Addquestion(q23id, ma_guestid, q23.Qname, q23.Answer, q23.Answer_1))
            {
                throw new System.Exception("ED01000380");
            }
            for (int i = 0; i < q3.Canswer.Count; i++)
            {
                if (q3.Canswer[i].ToString() != "")
                {
                    string answer = q3.Canswer[i].ToString();
                    string q3id = de_usquestion.GetNextSeqNumber() + q3.Qnumber + i.ToString();
                    if (!(new DE_USQUESTION(conn)).Addquestion(q3id, ma_guestid, q3.Qname, answer, q3.Answer_1))
                    {
                        throw new System.Exception("ED01000380");
                    }
                }
            }
            string q4id = de_usquestion.GetNextSeqNumber() + q4.Qnumber;
            if (!(new DE_USQUESTION(conn)).Addquestion(q4id, ma_guestid, q4.Qname, q4.Answer, q4.Answer_1))
            {
                throw new System.Exception("ED01000380");
            }
        }

        public void Updateregister(Question q5, Question q6, Question q7, Question q8, Question q9, Question q10, Question q11)
        {
            //传递提交信息
            DE_USQUESTION de_usquestion = new DE_USQUESTION(conn);
            string q5id = de_usquestion.GetNextSeqNumber() + q5.Qnumber;
            if (!(new DE_USQUESTION(conn)).Addquestion(q5id, ma_guestid, q5.Qname, q5.Answer, q5.Answer_1))
            {
                throw new System.Exception("ED01000380");
            }
            for (int i = 0; i < q6.Canswer.Count; i++)
            {
                if (q6.Canswer[i].ToString() != "")
                {
                    string answer = q6.Canswer[i].ToString();
                    string q6id = de_usquestion.GetNextSeqNumber() + q6.Qnumber + i.ToString();
                    if (!(new DE_USQUESTION(conn)).Addquestion(q6id, ma_guestid, q6.Qname, answer, q6.Answer_1))
                    {
                        throw new System.Exception("ED01000380");
                    }
                }
            }
            for (int i = 0; i < q7.Canswer.Count; i++)
            {
                if (q7.Canswer[i].ToString() != "")
                {
                    string answer = q7.Canswer[i].ToString();
                    string q7id = de_usquestion.GetNextSeqNumber() + q7.Qnumber + i.ToString();
                    if (!(new DE_USQUESTION(conn)).Addquestion(q7id, ma_guestid, q7.Qname, answer, q7.Answer_1))
                    {
                        throw new System.Exception("ED01000380");
                    }
                }
            }
            string q8id = de_usquestion.GetNextSeqNumber() + q8.Qnumber;
            if (!(new DE_USQUESTION(conn)).Addquestion(q8id, ma_guestid, q8.Qname, q8.Answer, q8.Answer_1))
            {
                throw new System.Exception("ED01000380");
            }
            string q9id = de_usquestion.GetNextSeqNumber() + q9.Qnumber;
            if (!(new DE_USQUESTION(conn)).Addquestion(q9id, ma_guestid, q9.Qname, q9.Answer, q9.Answer_1))
            {
                throw new System.Exception("ED01000380");
            }
            string q10id = de_usquestion.GetNextSeqNumber() + q10.Qnumber;
            if (!(new DE_USQUESTION(conn)).Addquestion(q10id, ma_guestid, q10.Qname, q10.Answer, q10.Answer_1))
            {
                throw new System.Exception("ED01000380");
            }
            string q11id = de_usquestion.GetNextSeqNumber() + q11.Qnumber;
            if (!(new DE_USQUESTION(conn)).Addquestion(q11id, ma_guestid, q11.Qname, q11.Answer, q11.Answer_1))
            {
                throw new System.Exception("ED01000380");
            }
        }
        public void Updaterepare(Question zb1, Question zb2, Question zb3, Question zb4, Question zb5)
        {
            //传递提交信息
            DE_USQUESTION de_usquestion = new DE_USQUESTION(conn);
            for (int i = 0; i < zb1.Canswer.Count; i++)
            {
                if (zb1.Canswer[i].ToString() != "")
                {
                    string answer = zb1.Canswer[i].ToString();
                    string zb1id = de_usquestion.GetNextSeqNumber() + zb1.Qnumber + i.ToString();
                    if (!(new DE_USQUESTION(conn)).Addquestion(zb1id, ma_guestid, zb1.Qname, answer, zb1.Answer_1))
                    {
                        throw new System.Exception("ED01000380");
                    }
                }
            }
            for (int i = 0; i < zb2.Canswer.Count; i++)
            {
                if (zb2.Canswer[i].ToString() != "")
                {
                    string answer = zb2.Canswer[i].ToString();
                    string zb2id = de_usquestion.GetNextSeqNumber() + zb2.Qnumber + i.ToString();
                    if (!(new DE_USQUESTION(conn)).Addquestion(zb2id, ma_guestid, zb2.Qname, answer, zb2.Answer_1))
                    {
                        throw new System.Exception("ED01000380");
                    }
                }
            }
            string zb3id = de_usquestion.GetNextSeqNumber() + zb3.Qnumber;
            if (!(new DE_USQUESTION(conn)).Addquestion(zb3id, ma_guestid, zb3.Qname, zb3.Answer, zb3.Answer_1))
            {
                throw new System.Exception("ED01000380");
            }
            for (int i = 0; i < zb4.Canswer.Count; i++)
            {
                if (zb4.Canswer[i].ToString() != "")
                {
                    string answer = zb4.Canswer[i].ToString();
                    string zb4id = de_usquestion.GetNextSeqNumber() + zb4.Qnumber + i.ToString();
                    if (!(new DE_USQUESTION(conn)).Addquestion(zb4id, ma_guestid, zb4.Qname, answer, zb4.Answer_1))
                    {
                        throw new System.Exception("ED01000380");
                    }
                }
            }
            string zb5id = de_usquestion.GetNextSeqNumber() + zb5.Qnumber;
            if (!(new DE_USQUESTION(conn)).Addquestion(zb5id, ma_guestid, zb5.Qname, zb5.Answer, zb5.Answer_1))
            {
                throw new System.Exception("ED01000380");
            }
        }
        public void Updateno(Question wz1, Question wz2, Question wz3)
        {
            //传递提交信息
            DE_USQUESTION de_usquestion = new DE_USQUESTION(conn);
            for (int i = 0; i < wz1.Canswer.Count; i++)
            {
                if (wz1.Canswer[i].ToString() != "")
                {
                    string answer = wz1.Canswer[i].ToString();
                    string wz1id = de_usquestion.GetNextSeqNumber() + wz1.Qnumber + i.ToString();
                    if (!(new DE_USQUESTION(conn)).Addquestion(wz1id, ma_guestid, wz1.Qname, answer, wz1.Answer_1))
                    {
                        throw new System.Exception("ED01000380");
                    }
                }
            }
            for (int i = 0; i < wz2.Canswer.Count; i++)
            {
                if (wz2.Canswer[i].ToString() != "")
                {
                    string answer = wz2.Canswer[i].ToString();
                    string wz2id = de_usquestion.GetNextSeqNumber() + wz2.Qnumber + i.ToString();
                    if (!(new DE_USQUESTION(conn)).Addquestion(wz2id, ma_guestid, wz2.Qname, answer, wz2.Answer_1))
                    {
                        throw new System.Exception("ED01000380");
                    }
                }
            }
            string wz3id = de_usquestion.GetNextSeqNumber() + wz3.Qnumber;
            if (!(new DE_USQUESTION(conn)).Addquestion(wz3id, ma_guestid, wz3.Qname, wz3.Answer, wz3.Answer_1))
            {
                throw new System.Exception("ED01000380");
            }
        }
        
        //保存提交数据
        public override void Save()
        {  
        }

        /// <summary>
        /// 取消更新 
        /// </summary>
        public override void Cancel()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        //传递ID值
        //public String Qname
        //{
        //    get { return qname; }
        //    set { qname = value; }
        //}
        //public String Answer
        //{
        //    get { return answer; }
        //    set { answer = value; }
        //}
        //public String Answer_1
        //{
        //    get { return answer_1; }
        //    set { answer_1 = value; }
        //}
        public String Username
        {
            get { return username; }
            set { username = value; }
        }
        public String Usertel
        {
            get { return usertel; }
            set { usertel = value; }
        }
        public String Userpost
        {
            get { return userpost; }
            set { userpost = value; }
        }
        public String Useremail
        {
            get { return useremail; }
            set { useremail = value; }
        }
        //public String  Qnubmer
        //{
        //    get { return qnubmer; }
        //    set { qnubmer = value; }
        //}
    }
}

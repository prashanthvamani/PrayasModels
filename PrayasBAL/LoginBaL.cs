using PrayasDAL;
using PrayasModels;
using System.Collections;
using System.Data;
using System.DirectoryServices;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using DirectoryEntry = System.DirectoryServices.DirectoryEntry;



namespace PrayasBAL
{
    public class LoginBaL
    {
        private string UserIdq = string.Empty;
        private string email = string.Empty;

        LoginDal loginDal = new LoginDal();

        //LoginSFDAL loginDal = new LoginSFDAL();

        public bool AuthenticateUser(string path, string user, string pass)
        {
            var de = new DirectoryEntry(path, user, pass);
            try
            {
                // var de = new DirectoryEntry(path, user, pass);
                var ds = new DirectorySearcher(de);
                ds.FindOne();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                de.Close();
            }
        }

        public DataSet BFILLogin(LoginModel data)
        {

            LogService("Step1");

            string finalresult = string.Empty;

            DataSet ds = new DataSet();
            //Utlity ut = new Utlity();


            int rc = 0;
            string uid1 = data.Username.ToString();
            uid1 = uid1.Substring(0, 3);

            int numericValue;
            bool isNumber = int.TryParse(uid1, out numericValue);

            LogService("Step2");


            if (isNumber == true)
            {


                Byte[] originalBytes;
                Byte[] encodedBytes;
                MD5 md5;

                md5 = new MD5CryptoServiceProvider();
                originalBytes = ASCIIEncoding.Default.GetBytes(data.Password.Trim());
                encodedBytes = md5.ComputeHash(originalBytes);

                string Enc = BitConverter.ToString(encodedBytes).Replace("-", "");


                data.Password = Enc;

                LogService("Step3");


                //rc = ndal.loginData(data);
            }
            if (rc == 1)
            {
                ds = loginDal.LoginBranches(data.Username);
                return ds;
            }

            else
            {
                LogService("Step4");

                try
                {

                    LogService("Step5");


                    string StrUser = "bfil\\" + data.Username;
                    string[] Result = StrUser.Split(new char[] { '\\' });
                    string StrDomainName;
                    string StrName;
                    if (Result.Length > 1)
                    {
                        StrDomainName = Result[0];
                        StrName = Result[1];
                        if (StrDomainName == "bfil")
                        {
                            //var Ldapconnectionbfil = ConfigurationManager.Connectio6nStrings["ADBFILConnection"].ToString();

                            ///DCPRADC
                            ///DCPRADC
                            //string Ldapconnectionbfil = "ldaps://DCPRAD03:636/DC=bfil,DC=local";


                            string Ldapconnectionbfil = "ldaps://DCPRAD03/DC=bfil,DC=local";
                            DirectorySearcher dssearch = new DirectorySearcher(Ldapconnectionbfil)
                            {
                                Filter = "(sAMAccountName=" + StrName + ")"
                            };

                            dssearch.SearchRoot.Path = "LDAP://DC=BFIL,DC=local";
                            SearchResult sresult = dssearch.FindOne();

                            Dictionary<string, ArrayList> userDetailsDic = new Dictionary<string, ArrayList>();

                            if (sresult != null)
                            {
                                foreach (string propertyName in sresult.Properties.PropertyNames)
                                {
                                    ArrayList Results = new ArrayList();

                                    foreach (object myCollection in sresult.Properties[propertyName])
                                    {
                                        Results.Add(myCollection);
                                    }

                                    userDetailsDic.Add(propertyName, Results);
                                }

                                foreach (KeyValuePair<string, ArrayList> detail in userDetailsDic)
                                {
                                    foreach (object value in detail.Value)
                                    {
                                        if (detail.Key == "description")
                                        {
                                            UserIdq = value.ToString();
                                            // LogService("AuthenticateUser " + UserIdq);

                                        }

                                        if (detail.Key == "mail")
                                        {
                                            email = value.ToString();

                                            //  LogService("AuthenticateUser " + email);
                                        }

                                    }

                                }


                                LogService("Step6");


                                DirectoryEntry dsresult = sresult.GetDirectoryEntry();
                                string ldappath = dsresult.Path;


                                string LineOfText;

                                string[] ArryText;
                                string Split_LdapPath = "";
                                string word = ldappath;

                                word = word.Replace("sksindia", "BFIL");
                                LineOfText = word;
                                ArryText = LineOfText.Split(new[] { "//" }, StringSplitOptions.None);

                                Split_LdapPath = ArryText[1];

                                ///DCPRADC
                                //string FinalPath = "LDAP://DCPRAD03/" + Split_LdapPath;
                                string FinalPath = "LDAP://DCPRAD03/" + Split_LdapPath;
                                // LogService("AuthenticateUser " + FinalPath);

                                bool aa = AuthenticateUser(FinalPath, data.Username, data.Password);


                                LogService("Step7");

                                if (aa)
                                {
                                    LogService("Step8");

                                    string uid = UserIdq;
                                    if (uid.Length >= 3)
                                    {
                                        uid = uid.Substring(0, 3);
                                        if (uid.ToUpper() != "VND")
                                        {
                                            LogService("Step9");

                                            ds = loginDal.LoginBranches(UserIdq);

                                            LogService("Step10");

                                            return ds;
                                        }
                                        else
                                        {
                                            //ds = ndal.LoginBranchesVendor(UserIdq);

                                            return ds;
                                        }
                                    }
                                    else if (uid.Length == 2)
                                    {
                                        ds = loginDal.LoginBranches(UserIdq);
                                        return ds;

                                    }
                                }
                                else
                                {
                                    finalresult = "E203#Invalid Password";
                                }
                            }
                            else
                            {
                                finalresult = "E204#Invalid User Name";
                            }
                        }
                        else
                        {
                            finalresult = "E205#Please prefix BFIL to userName";
                        }
                    }
                }


                catch (System.Threading.ThreadAbortException ex)
                {
                    finalresult = "E206#" + ex.Message;

                }
                catch (Exception ex)
                {
                    finalresult = "E207#" + ex.Message;


                }
                DataTable dtt = convertStringToDataTable(finalresult);

                ds.Tables.Add(dtt);

                return ds;
            }

        }


        private void LogService(string content)
        {
            try
            {

                string folderPath = @"C:\ErrorLoginBAL";
                string filenames = folderPath + "\\Log_ " + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }

                if (!System.IO.File.Exists(filenames))
                {
                    FileStream fs1 = new FileStream(filenames, FileMode.Create, FileAccess.Write);
                    fs1.Close();
                }

                FileStream fs = new FileStream(filenames, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable convertStringToDataTable(string data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmployeeID");
            foreach (string value in data.Split('#'))
            {
                dt.Rows.Add(value);
            }
            return dt;
        }
    }
}

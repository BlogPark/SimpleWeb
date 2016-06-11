using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// UploadHandler 的摘要说明
/// </summary>
public class UploadHandler : Handler
{

    public UploadConfig UploadConfig { get; private set; }
    public UploadResult Result { get; private set; }

    public UploadHandler(HttpContext context, UploadConfig config)
        : base(context)
    {
        this.UploadConfig = config;
        this.Result = new UploadResult() { State = UploadState.Unknown };
    }

    public override void Process()
    {
        byte[] uploadFileBytes = null;
        string uploadFileName = null;

        if (UploadConfig.Base64)
        {
            uploadFileName = UploadConfig.Base64Filename;
            uploadFileBytes = Convert.FromBase64String(Request[UploadConfig.UploadFieldName]);
        }
        else
        {
            var file = Request.Files[UploadConfig.UploadFieldName];
            uploadFileName = file.FileName;

            if (!CheckFileType(uploadFileName))
            {
                Result.State = UploadState.TypeNotAllow;
                WriteResult();
                return;
            }
            if (!CheckFileSize(file.ContentLength))
            {
                Result.State = UploadState.SizeLimitExceed;
                WriteResult();
                return;
            }

            uploadFileBytes = new byte[file.ContentLength];
            try
            {
                file.InputStream.Read(uploadFileBytes, 0, file.ContentLength);
            }
            catch (Exception)
            {
                Result.State = UploadState.NetworkError;
                WriteResult();
            }
        }

        Result.OriginFileName = uploadFileName;
        string currentType = Path.GetExtension(uploadFileName);
        string savePath = UploadConfig.PathFormat.Replace('/','\\').TrimEnd('/').TrimEnd('\\') + "/ueimg/" + CreateFolder(DateTime.Now)+ "/";
        //判断保持路径是否存在
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
        else
        {
            string newSavePath = CreateChildDir(savePath);
            if (newSavePath != savePath)
            {
                Directory.CreateDirectory(newSavePath);
                savePath = newSavePath;
            }
        }
        string filename =  CreateImageFileName() + currentType;
        //var savePath = PathFormatter.Format(uploadFileName, UploadConfig.PathFormat);
        //var localPath = Server.MapPath(savePath);
        string localPath =savePath + filename;
        try
        {
            File.WriteAllBytes(localPath, uploadFileBytes);
            Result.Url = localPath.Replace(UploadConfig.PathFormat.Replace('/', '\\').TrimEnd('/').TrimEnd('\\'),""); 
            Result.State = UploadState.Success;
        }
        catch (Exception e)
        {
            Result.State = UploadState.FileAccessError;
            Result.ErrorMessage = e.Message;
        }
        finally
        {
            WriteResult();
        }
    }

    private void WriteResult()
    {
        this.WriteJson(new
        {
            state = GetStateMessage(Result.State),
            url = Result.Url,
            title = Result.OriginFileName,
            original = Result.OriginFileName,
            error = Result.ErrorMessage
        });
    }

    private string GetStateMessage(UploadState state)
    {
        switch (state)
        {
            case UploadState.Success:
                return "SUCCESS";
            case UploadState.FileAccessError:
                return "文件访问出错，请检查写入权限";
            case UploadState.SizeLimitExceed:
                return "文件大小超出服务器限制";
            case UploadState.TypeNotAllow:
                return "不允许的文件格式";
            case UploadState.NetworkError:
                return "网络错误"; 
        }
        return "未知错误";
    }

    private bool CheckFileType(string filename)
    {
        var fileExtension = Path.GetExtension(filename).ToLower();
        return UploadConfig.AllowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
    }

    private bool CheckFileSize(int size)
    {
        return size < UploadConfig.SizeLimit;
    }

    /// <summary>
    /// 生成文件夹
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string CreateFolder(DateTime dt)
    {
        string folder = dt.ToString("yyyyMMdd");
        Dictionary<string, string> numericDict = NumericDict();
        foreach (var kv in numericDict)
            folder = folder.Replace(kv.Key, kv.Value);
        return folder;
    }
    private static Dictionary<string, string> NumericDict()
    {
        var dict = new Dictionary<string, string>();
        dict.Add("0", "X");
        dict.Add("1", "I");
        dict.Add("2", "II");
        dict.Add("3", "3");
        dict.Add("4", "IV");
        dict.Add("5", "V");
        dict.Add("6", "VI");
        dict.Add("7", "7");
        dict.Add("8", "H");
        dict.Add("9", "IX");
        return dict;
    }
    /// <summary>
    /// 生成二级文件夹名
    /// </summary>
    /// <param name="savePath"></param>
    /// <param name="maxFile"></param>
    /// <returns></returns>
    public static string CreateChildDir(string savePath, int maxFile = 1000)
    {
        //声明并初始化DirectoryInfo对象
        DirectoryInfo dirInfo = new DirectoryInfo(savePath);
        //获取文件夹下所有的文件
        FileInfo[] files = dirInfo.GetFiles();
        //判断文件数量是否达到maxFile的上限
        if (files.Count() > maxFile)
        {
            //读取子文件夹
            DirectoryInfo[] childDir = dirInfo.GetDirectories();
            Int32 childNo = 1;
            //判断子文件夹是否存在
            if (childDir != null && childDir.Any())
            {
                //获取最后一个创建的子文件
                var myChildDir = childDir.Where(m => System.Text.RegularExpressions.Regex.IsMatch(m.Name, @"\d+"));
                if (myChildDir != null && myChildDir.Any())
                {
                    DirectoryInfo lastDir = myChildDir.OrderBy(m => m.CreationTime).Last();
                    //判断子文件夹下的文件重量是否达到maxFile上限
                    if (lastDir.GetFiles() != null && lastDir.GetFiles().Any())
                    {
                        string childDirName = lastDir.Name;

                        if (lastDir.GetFiles().Count() > maxFile)
                        {
                            childNo = Convert.ToInt32(childDirName) + 1;
                            savePath = string.Format("{0}/{1}/", savePath.Trim('/'), childNo);
                        }
                        else
                        {
                            savePath = string.Format("{0}/{1}/", savePath.Trim('/'), childDirName);
                        }
                    }
                    else
                    {
                        savePath = string.Format("{0}/{1}/", savePath.Trim('/'), childNo);
                    }
                }
                else
                {
                    savePath = string.Format("{0}/{1}/", savePath.Trim('/'), childNo);
                }
            }
            else
            {
                savePath = string.Format("{0}/{1}/", savePath.Trim('/'), childNo);
            }
        }
        return savePath;
    }
    /// <summary>
    /// 生成文件名称
    /// </summary>
    /// <returns></returns>
    public static string CreateImageFileName()
    {
        string treadName = System.Threading.Thread.CurrentThread.Name;
        string fix = "";
        if (!string.IsNullOrEmpty(treadName) && treadName.IndexOf("?") != -1)
        {
            fix = treadName.Split('?')[1];
        }
        string ticks = DateTime.Now.Ticks.ToString();
        Dictionary<string, string> numericDict = NumericDict();
        foreach (var kv in numericDict)
            ticks = ticks.Replace(kv.Key, kv.Value);
        return string.Format("{0}{1}", ticks, fix);
    }
}

public class UploadConfig
{
    /// <summary>
    /// 文件命名规则
    /// </summary>
    public string PathFormat { get; set; }

    /// <summary>
    /// 上传表单域名称
    /// </summary>
    public string UploadFieldName { get; set; }

    /// <summary>
    /// 上传大小限制
    /// </summary>
    public int SizeLimit { get; set; }

    /// <summary>
    /// 上传允许的文件格式
    /// </summary>
    public string[] AllowExtensions { get; set; }

    /// <summary>
    /// 文件是否以 Base64 的形式上传
    /// </summary>
    public bool Base64 { get; set; }

    /// <summary>
    /// Base64 字符串所表示的文件名
    /// </summary>
    public string Base64Filename { get; set; }
}

public class UploadResult
{
    public UploadState State { get; set; }
    public string Url { get; set; }
    public string OriginFileName { get; set; }

    public string ErrorMessage { get; set; }
}

public enum UploadState
{
    Success = 0,
    SizeLimitExceed = -1,
    TypeNotAllow = -2,
    FileAccessError = -3,
    NetworkError = -4,
    Unknown = 1,
}


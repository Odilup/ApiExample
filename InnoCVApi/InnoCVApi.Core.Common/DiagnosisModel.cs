namespace InnoCVApi.Core.Common
{
    public class DiagnosisModel
    {
        public int ErrorCode { get; set; }

        public DiagnosisModel(int errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}
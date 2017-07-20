using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapExplorer.Core
{
    /// <summary>
    /// Sap 결과
    /// </summary>
    public class SapResult<T> where T : class, new()
    {
        /// <summary>
        /// 성공여부
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// 메시지
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 익스포트
        /// </summary>
        public T Export { get; set; } = new T();

        /// <summary>
        /// 오류 추가
        /// </summary>
        /// <param name="message">메시지</param>
        public void AddError(string message)
        {
            this.Success = false;
            this.Message = message;
        }

        /// <summary>
        /// 오류 추가
        /// </summary>
        /// <param name="ex">예외</param>
        public void AddError(Exception ex)
        {
            this.Success = false;
            this.Message = ex.Message;
        }
    }
}

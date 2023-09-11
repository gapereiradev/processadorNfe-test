using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTOS
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }

        public bool HasError => this.Errors != null ? true : false;
        public bool HasWarning => Warnings is not null;
        public List<string> Errors { get; set; }
        public List<string> Warnings { get; set; }

        public string AccessToken { get; set; }

        public ResponseModel()
        {
        }

        public ResponseModel(T data)
        {
            this.Data = data;
        }

        public ResponseModel<T> AddError(string errorMessage)
        {
            if (this.Errors == null)
                this.Errors = new List<string>();
            this.Errors.Add(errorMessage);
            return this;
        }

        public ResponseModel<T> AddError(List<string> errors)
        {
            if (this.Errors == null)
                this.Errors = new List<string>();
            this.Errors.AddRange(errors);
            return this;
        }
        public ResponseModel<T> AddWarning(string warning)
        {
            if (this.Warnings == null)
                this.Warnings = new List<string>();
            this.Warnings.Add(warning);
            return this;
        }
        public ResponseModel<T> AddWarnings(List<string> warnings)
        {
            if (this.Warnings == null)
                this.Warnings = new List<string>();
            this.Warnings.AddRange(warnings);
            return this;
        }

        public void AddData(T data)
        {
            if (this.Data == null || Data.Equals(Guid.Parse("00000000-0000-0000-0000-000000000000")))
                this.Data = data;
        }
    }
}

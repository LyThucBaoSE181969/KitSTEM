using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.Services
{
    public class ResponseService
    {
        public bool Succeeded { get; private set; }
        public string Status { get; private set; }
        public int StatusCode { get; private set; } = 500;
        public Dictionary<string, object>? Details { get; private set; }

        public ResponseService()
        {
            Succeeded = true;
            Status = "success";
        }

        public ResponseService SetSucceeded(bool status)
        {
            Status = status ? "success" : "fail";

            Succeeded = status;
            return this;
        }
        public ResponseService AddDetail(string key, object value)
        {
            if (Details == null)
            {
                Details = new Dictionary<string, object>();
            }
            Details.Add(ToKebabCase(key), value);
            return this;
        }

        // Dùng để add errors
        public ResponseService AddError(string key, string value)
        {
            if (Details == null)
            {
                Details = new Dictionary<string, object>();
            }
            if (!Details.ContainsKey("errors"))
            {
                Details.Add("errors", new Dictionary<string, string>());
            }

            var errors = (Dictionary<string, string>)Details["errors"];
            errors.Add(ToKebabCase(key), value);

            return this;
        }

        public ResponseService SetStatusCode(int code)
        {
            StatusCode = code;
            return this;
        }

        public static string ToKebabCase(string input)
        {
            return string.Concat(input.Select((c, i) =>
                i > 0 && char.IsUpper(c) ? "-" + char.ToLower(c).ToString() : char.ToLower(c).ToString()));
        }
    }
}

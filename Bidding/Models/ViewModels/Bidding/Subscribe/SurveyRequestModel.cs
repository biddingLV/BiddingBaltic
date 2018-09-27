using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Subscribe
{
    public class SurveyRequestModel
    {
        public string Name { get; set; }
        public List<string> Categories { get; set; }
        public string InterestsQuestion { get; set; }
        public List<string> NewsLetterOptions { get; set; }
        public string FrequencyQuestion { get; set; }
        public string VisualQuestion { get; set; }
        public int MoneyQuestion { get; set; }
        public string StatisticQuestion { get; set; }
        public string ConsultationQuestion { get; set; }
    }
}

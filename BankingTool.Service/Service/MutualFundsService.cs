using BankingTool.Model;
using BankingTool.Model.Dto.MutualFunds;
using BankingTool.Model.Model;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class MutualFundsService(HttpClient httpClient, IMutualFundsRepository mutualFundsRepository) : IMutualFundsService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IMutualFundsRepository _mutualFundsRepository = mutualFundsRepository;

        public async Task<ResponseDto<List<MutualFundsDto>>> GetAllMutualFundsFromExternal()
        {
            var url = "https://www.amfiindia.com/spages/NAVAll.txt";
            var responseFunds = await _httpClient.GetAsync(url);
            var response = await responseFunds.Content.ReadAsStringAsync();
            var lines = response.Split('\n');
            var funds = new List<MutualFundsDto>();

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length < 8) continue; // skip headers or invalid lines

                if (!decimal.TryParse(parts[6].Trim(), out var navValue))
                    continue; // skip if NAV is not valid

                funds.Add(new MutualFundsDto
                {
                    SchemeCode = parts[0].Trim(),
                    ISINDivPayout = parts[1].Trim(),
                    ISINGrowth = parts[2].Trim(),
                    ISINDivReinvestment = parts[3].Trim(),
                    SchemeName = parts[4].Trim() + " - " + parts[5].Trim(), // combine Plan + Option for clarity
                    MutualFundValue = navValue,
                    MutualFundDate = DateTime.TryParse(parts[7].Trim(), out var date) ? date : DateTime.MinValue
                });
            }


            var result = new ResponseDto<List<MutualFundsDto>>
            {
                DropDownList = [],
                Errors = [],
                Response = funds,
                StatuCode = 200,
                Status = true,
                ValidationErrors = []
            };
            return result;
        }

        public async Task<ResponseDto<List<MutualFundsDto>>> GetAllMutualFundsInDB()
        {
            var funds = await _mutualFundsRepository.GetAllScheme();
            var schemes = funds.Select(f => new MutualFundsDto
            {
                SchemeCode = f.SchemeCode,
                ISINDivPayout = f.ISINDivPayout,
                ISINGrowth = f.ISINGrowth,
                ISINDivReinvestment = f.ISINDivReinvestment,
                SchemeName = f.SchemeName,
                MutualFundValue = f.MutualFunds.OrderByDescending(mf => mf.MutualFundDate).FirstOrDefault()?.MutualFundValue ?? 0,
                MutualFundDate = f.MutualFunds.OrderByDescending(mf => mf.MutualFundDate).FirstOrDefault()?.MutualFundDate ?? DateTime.MinValue
            }).ToList();
            var result = new ResponseDto<List<MutualFundsDto>>
            {
                DropDownList = [],
                Errors = [],
                Response = schemes,
                StatuCode = 200,
                Status = true,
                ValidationErrors = []
            };
            return result;
        }

        public async Task<ResponseDto<List<MutualFundsDto>>> GetMutualFundsByCodeFromExternal(List<string> codes)
        {
            var url = "https://www.amfiindia.com/spages/NAVAll.txt";
            var responseFunds = await _httpClient.GetAsync(url);
            var response = await responseFunds.Content.ReadAsStringAsync();
            var lines = response.Split('\n');
            var funds = new List<MutualFundsDto>();

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length < 6 || !decimal.TryParse(parts[4], out var navValue))
                    continue; // skip headers or invalid lines

                funds.Add(new MutualFundsDto
                {
                    SchemeCode = parts[0].Trim(),
                    ISINDivPayout = parts[1].Trim(),
                    ISINGrowth = parts[2].Trim(),
                    ISINDivReinvestment = parts[3].Trim(),
                    SchemeName = parts[3].Trim(), // sometimes scheme name is in 4th column
                    MutualFundValue = navValue,
                    MutualFundDate = DateTime.TryParse(parts[5].Trim(), out var date) ? date : DateTime.MinValue
                });
            }

            var iciciMutualFunds = funds.Where(x => codes.Contains(x.SchemeCode)).ToList();
            //var iciciMutualFunds = funds.Where(x => x.SchemeCode == "148989" || x.SchemeCode == "153610" || x.SchemeCode == "149182").ToList();

            var result = new ResponseDto<List<MutualFundsDto>>
            {
                DropDownList = [],
                Errors = [],
                Response = iciciMutualFunds,
                StatuCode = 200,
                Status = true,
                ValidationErrors = []
            };
            return result;
        }

        public async Task<ResponseDto<bool>> InsertSchemes(List<string> codes)
        {
            var url = "https://www.amfiindia.com/spages/NAVAll.txt";
            var responseFunds = await _httpClient.GetAsync(url);
            var response = await responseFunds.Content.ReadAsStringAsync();
            var lines = response.Split('\n');
            var funds = new List<MutualFundsDto>();

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length < 8) continue; // skip headers or invalid lines

                if (!decimal.TryParse(parts[6].Trim(), out var navValue))
                    continue; // skip if NAV is not valid

                funds.Add(new MutualFundsDto
                {
                    SchemeCode = parts[0].Trim(),
                    ISINDivPayout = parts[1].Trim(),
                    ISINGrowth = parts[2].Trim(),
                    ISINDivReinvestment = parts[3].Trim(),
                    SchemeName = parts[4].Trim() + " - " + parts[5].Trim(), // combine Plan + Option for clarity
                    MutualFundValue = navValue,
                    MutualFundDate = DateTime.TryParse(parts[7].Trim(), out var date) ? date : DateTime.MinValue
                });
            }

            var schemes = funds.Where(x => codes.Contains(x.SchemeCode)).ToList();

            foreach (var scheme in schemes)
            {
                var existingScheme = await _mutualFundsRepository.GetSchemeByCode(scheme.SchemeCode);
                if (existingScheme == null)
                {
                    var newScheme = new Scheme
                    {
                        SchemeCode = scheme.SchemeCode,
                        ISINDivPayout = scheme.ISINDivPayout,
                        ISINGrowth = scheme.ISINGrowth,
                        ISINDivReinvestment = scheme.ISINDivReinvestment,
                        SchemeName = scheme.SchemeName
                    };
                    var schemeId = await _mutualFundsRepository.AddScheme(newScheme);

                    var mutualFund = new MutualFunds
                    {
                        MutualFundValue = scheme.MutualFundValue,
                        MutualFundDate = scheme.MutualFundDate,
                        SchemeId = schemeId.Value
                    };
                    await _mutualFundsRepository.AddMutualFunds(mutualFund);
                }
                else
                {
                    var existingMutualFund = await _mutualFundsRepository.GetLastMutualFundsBySchemeId(existingScheme.SchemeId);
                    var date = DateTime.Now;
                    var date1 = DateTime.Now.AddDays(-1);
                    if (existingMutualFund.MutualFundDate.Date != date.Date && existingMutualFund.MutualFundDate.Date != date1.Date)
                    {

                        var mutualFund = new MutualFunds
                        {
                            MutualFundValue = scheme.MutualFundValue,
                            MutualFundDate = date,
                            SchemeId = existingScheme.SchemeId
                        };
                        await _mutualFundsRepository.AddMutualFunds(mutualFund);
                    }
                }
            }
            var result = new ResponseDto<bool>
            {
                DropDownList = [],
                Errors = [],
                Response = true,
                StatuCode = 200,
                Status = true,
                ValidationErrors = []
            };
            return result;
        }

        public async Task<ResponseDto<List<UserInvestmentDto>>> GetAllUserInvestment()
        {
            var userInvestments = await _mutualFundsRepository.GetUserInvestmentByUserId();

            var response =  userInvestments.Select(x => new UserInvestmentDto 
            { 
                InvestmentId = x.InvestmentId,
                SchemeCode = x.Scheme.SchemeCode,
                SchemeName = x.Scheme.ISINDivReinvestment + x.Scheme.SchemeName,
                InstallmentAmount = x.InstallmentAmount,
                UnitsPurchased = x.UnitsPurchased,
                PurchaseDate = x.PurchaseDate,
                MutualFundAtPurchase = x.MutualFundAtPurchase,
                MutualFundCurrentValue = x.Scheme.MutualFunds.OrderByDescending(a => a.MutualFundDate).FirstOrDefault(b => b.SchemeId == x.SchemeId).MutualFundValue
            }).ToList();
            var result = new ResponseDto<List<UserInvestmentDto>>
            {
                DropDownList = [],
                Errors = [],
                Response = response,
                StatuCode = 200,
                Status = true,
                ValidationErrors = []
            };
            return result;
        }

        public async Task<ResponseDto<bool>> InsertUserInvestment(List<string> codes, decimal installmentAmount)
        {
            foreach (var code in codes)
            {
                var scheme = await _mutualFundsRepository.GetSchemeByCode(code);
                var mutualFund = await _mutualFundsRepository.GetLastMutualFundsBySchemeId(scheme.SchemeId);
                var existingUserInvestment = await _mutualFundsRepository.GetUserInvestmentByUserId();
                var month = DateTime.Now.Month;

                if (scheme != null && mutualFund != null && !existingUserInvestment.Any(x => x.SchemeId == scheme.SchemeId && x.PurchaseDate.Month == month))
                {
                    var userInvestment = new UserInvestment
                    {
                        SchemeId = scheme.SchemeId,
                        InstallmentAmount = installmentAmount,
                        PurchaseDate = DateTime.Now,
                        UnitsPurchased = installmentAmount / mutualFund.MutualFundValue,
                        MutualFundAtPurchase = mutualFund.MutualFundValue,
                    };
                    await _mutualFundsRepository.AddUserInvestment(userInvestment);
                }
            }
            var result = new ResponseDto<bool>
            {
                DropDownList = [],
                Errors = [],
                Response = true,
                StatuCode = 200,
                Status = true,
                ValidationErrors = []
            };
            return result;
        }

        public async Task<ResponseDto<bool>> InsertDailyMutualFundValue()
        {
            var funds = await _mutualFundsRepository.GetAllScheme();
            foreach (var fund in funds)
            {
                var lastMutualFund = await _mutualFundsRepository.GetLastMutualFundsBySchemeId(fund.SchemeId);
                var schemeCode = fund.SchemeCode;
                var yesterday = DateTime.Now.AddDays(-1).Date;
                var lastMutualFundDate = lastMutualFund!.MutualFundDate.Date;
                var lastMutualFundDatePlusOne = lastMutualFundDate.AddDays(1).Date;
                var url = $"https://api.tigzig.com/mf/v1/nav?scheme={schemeCode}&since={lastMutualFundDatePlusOne.ToString("yyyy-MM-dd")}&to={yesterday.ToString("yyyy-MM-dd")}";

                var responseFunds = await _httpClient.GetAsync(url);
                var response = await responseFunds.Content.ReadAsStringAsync();
                var navResponse = System.Text.Json.JsonSerializer.Deserialize<MutualFundDailyResponse>(response);

                foreach (var nav in navResponse!.Data)
                {
                    var mutualFund = new MutualFunds
                    {
                        MutualFundValue = nav.Nav,
                        MutualFundDate = nav.Date,
                        SchemeId = fund.SchemeId
                    };
                    await _mutualFundsRepository.AddMutualFunds(mutualFund);
                }
            }
            var result = new ResponseDto<bool>
            {
                DropDownList = [],
                Errors = [],
                Response = true,
                StatuCode = 200,
                Status = true,
                ValidationErrors = []
            };
            return result;
        }

    }
}

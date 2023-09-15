using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.VisualBasic;
using MudBlazor;
using System.Globalization;

namespace CleaveMudDateMask.Component
{
    public class MudDateField : MudDatePicker
    {
        [Parameter] public string idp { get; set; } = default!;
        [Inject] protected IJSRuntime _jsRuntime { get; set; } = default!;

        public MudDateField()
        {
            UserAttributes?.Add("data-muddate", "true");
            DateFormat = "yyyy.MM.dd";
            Editable = true;
            ImmediateText = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var options = new
                {
                    date= true,
                    datePattern = new[] { "Y", "m", "d" },
                    delimiter = '.'
                };

                await _jsRuntime.InvokeVoidAsync("mudDatePickerMask", idp, options);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected override Task StringValueChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (DateTime.TryParseExact(value, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validDate))
                {
                    base.Date = validDate;
                }
            }

            return base.StringValueChanged(value);
        }
    }
}

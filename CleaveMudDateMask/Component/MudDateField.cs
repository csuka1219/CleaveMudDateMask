using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.VisualBasic;
using MudBlazor;

namespace CleaveMudDateMask.Component
{
    public class MudDateField : MudDatePicker
    {
        [Parameter] public string idp { get; set; }
        [Inject] protected IJSRuntime _jsRuntime { get; set; }

        public MudDateField()
        {
            if (UserAttributes != null)
            {
                UserAttributes.Add("data-muddate", "true");
            }
            DateFormat = "yyyy.MM.dd";
            Editable = true;
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
    }
}

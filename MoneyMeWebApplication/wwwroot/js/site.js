
$(function () {
    var sliderAmount = document.getElementById("amountRange");
    var sliderTerm = document.getElementById("termRange");
    var amountOutput = document.getElementById("amountLabel");
    var termOutput = document.getElementById("termLabel");
    var sliderAmountTooltip = document.getElementById("amountSliderTooltip");
    var sliderTermTooltip = document.getElementById("termSliderTooltip");

    amountOutput.innerHTML = sliderAmount.value; // Display the default slider value
    termOutput.innerHTML = sliderTerm.value; // Display the default slider value

    sliderAmount.oninput = function () {
        sliderAmountTooltip.innerHTML = '$' +this.value;
        amountOutput.innerHTML = this.value;
        const minValue = sliderAmount.min;
        const maxValue = sliderAmount.max;
        const totalInputWidth = sliderAmount.offsetWidth;
        const thumbHalfWidth = 12.5;

        const left = (((this.value - minValue) / (maxValue - minValue )) * ((totalInputWidth - thumbHalfWidth) - thumbHalfWidth)) + thumbHalfWidth;
        var value = (this.value - this.min) / (this.max - this.min) * 100
        this.style.background = 'linear-gradient(to right, #82CFD0 0%, #82CFD0 ' + value + '%, #d3d3d3 ' + value + '%, #d3d3d3 100%)'
        sliderAmountTooltip.style.left = left + 'px';
    };

    sliderTerm.oninput = function () {
        sliderTermTooltip.innerHTML = this.value;
        const minValue = sliderTerm.min;
        const maxValue = sliderTerm.max;
        const totalInputWidth = sliderTerm.offsetWidth;
        const thumbHalfWidth = 12.5;

        const left = (((this.value - minValue) / (maxValue - minValue)) * ((totalInputWidth - thumbHalfWidth) - thumbHalfWidth)) + thumbHalfWidth;
        termOutput.innerHTML = this.value;
        var value = (this.value - this.min) / (this.max - this.min) * 100
        this.style.background = 'linear-gradient(to right, #82CFD0 0%, #82CFD0 ' + value + '%, #d3d3d3 ' + value + '%, #d3d3d3 100%)'
        sliderTermTooltip.style.left = left + 'px';
    };

    if (amountOutput.innerHTML !== "") {
        sliderAmountTooltip.innerHTML = '$' + sliderAmount.value;
        const minValue = sliderAmount.min;
        const maxValue = sliderAmount.max;
        const totalInputWidth = sliderAmount.offsetWidth;
        const thumbHalfWidth = 12.5;
        const left = (((sliderAmount.value - minValue) / (maxValue - minValue)) * ((totalInputWidth - thumbHalfWidth) - thumbHalfWidth)) + thumbHalfWidth;
        var value = (sliderAmount.value - sliderAmount.min) / (sliderAmount.max - sliderAmount.min) * 100

        sliderAmount.style.background = 'linear-gradient(to right, #82CFD0 0%, #82CFD0 ' + value + '%, #d3d3d3 ' + value + '%, #d3d3d3 100%)'
        sliderAmountTooltip.style.left = left + 'px';
    }

    if (termOutput.innerHTML !== "") {
        sliderTermTooltip.innerHTML = sliderTerm.value;
        const minValue = sliderTerm.min;
        const maxValue = sliderTerm.max;
        const totalInputWidth = sliderTerm.offsetWidth;
        const thumbHalfWidth = 12.5;
        const left = (((sliderTerm.value - minValue) / (maxValue - minValue)) * ((totalInputWidth - thumbHalfWidth) - thumbHalfWidth)) + thumbHalfWidth;
        var value = (sliderTerm.value - sliderTerm.min) / (sliderTerm.max - sliderTerm.min) * 100

        sliderTerm.style.background = 'linear-gradient(to right, #82CFD0 0%, #82CFD0 ' + value + '%, #d3d3d3 ' + value + '%, #d3d3d3 100%)'
        sliderTermTooltip.style.left = left + 'px';
    }
})



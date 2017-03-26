
var progressPecent = (Math.floor((currentTotal / targetAmount) * 100));
if(progressPecent>= 100) {
    progressPecent = 100
}
document.getElementById("progress-bar").style.width = progressPecent + "%";
document.getElementById("progress-bar").innerHTML = progressPecent + "% to goal!";
document.getElementById("currentTotal").innerHTML = "$" + currentTotal;
document.getElementById("targetAmount").innerHTML = "$" + targetAmount;
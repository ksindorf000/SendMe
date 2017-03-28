//stripe payment start
var stripe = Stripe(pubKey);
var elements = stripe.elements();
function checkEmail(emailAddress) {
    var validEmail = /^[a-z0-9\.\_%+-]+@@[a-z0-9\.\-]+\.[a-z]{2,4}$/i;
    if (emailAddress.search(validEmail)) {
        return false;
    }
    else {
        return true;
    }
}


var card = elements.create('card', {
    style: {
        base: {
            iconColor: '#666EE8',
            color: '#31325F',
            lineHeight: '40px',
            fontWeight: 300,
            fontFamily: 'Helvetica Neue',
            fontSize: '15px',

            '::placeholder': {
                color: '#CFD7E0',
            },
        },
    }
});
card.mount('#card-element');

function setOutcome(result) {

    var errorElement = document.querySelector('.error');
    errorElement.classList.remove('visible');

    if (result.token) {

        var amount = $("#DonationAmount").val() * 100;
        var Name = $("#DonorName").val();
        var Email = $("#DonorEmail").val();
        var Phone = $("#DonorPhoneNumber").val();
        var donationMsg = $("#donationMsg").val();
        window.location.href = "/Donation/Payment?stripeToken=" + result.token.id + "&amount=" + amount + "&Name=" + Name + "&Email=" + Email + "&Phone=" + Phone + "&tripId=" + tripId + "&userName=" + userName + "&donationMsg=" + donationMsg;

    } else if (result.error) {
        errorElement.textContent = result.error.message;
        errorElement.classList.add('visible');
    }
}

card.on('change', function (event) {
    setOutcome(event);
});

document.querySelector('form[name="DonationForm"]').addEventListener('submit', function (e) {
    e.preventDefault();
    var form = document.querySelector('form[name="DonationForm"]');
    var extraDetails = {
        name: form.querySelector('input[name=DonorName]').value,
    };
    stripe.createToken(card, extraDetails).then(setOutcome);
});
//stripe payment end

//Show and hide the modals for payment success/fail
$("#paymentSuccessToggle").hide();
$("#successMsgModal").modal('hide');
$("#paymentFailedToggle").hide();

if(paymentMsg.indexOf('Payment Successful') >= 0)
{
    $("#paymentSuccessToggle").modal('show');
    $("#successMsgModal").modal('show');
}
if(paymentMsg.indexOf('problem') >= 0)
{
    $("#paymentFailedToggle").modal('show');
    $("#failedMsgModal").modal('show');
}
   
$("#approveMsgBtn").click(function () {
    window.location.href = "/send/"+userName;
});
$("#declineMsgBtn").click(function () {
    window.location.href = "/send/" + userName;
});

//refresh page
$("#paymentBackBtn").click(function () {
    location.reload();
});


//shows tooltips
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

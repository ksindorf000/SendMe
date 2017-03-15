
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
        window.location.href = "/Donation/Payment?stripeToken=" + result.token.id + "&amount=" + amount + "&Name=" + Name + "&Email=" + Email + "&Phone=" + Phone + "&tripId=" + tripId;



    } else if (result.error) {
        errorElement.textContent = result.error.message;
        errorElement.classList.add('visible');
    }
}

card.on('change', function (event) {
    setOutcome(event);
});

document.querySelector('form').addEventListener('submit', function (e) {
    e.preventDefault();
    var form = document.querySelector('form');
    var extraDetails = {
        name: form.querySelector('input[name=DonorName]').value,
    };
    stripe.createToken(card, extraDetails).then(setOutcome);
});

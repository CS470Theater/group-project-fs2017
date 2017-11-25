$(function () {
    function data(key) { return $('#data').data(key); }

    braintree.client.create({
        authorization: data('client-token')
    }, function (clientErr, clientInstance) {
        if (clientErr) {
            console.error(clientErr);
            return;
        }
        
        braintree.paypalCheckout.create({
            client: clientInstance
        }, function (paypalCheckoutErr, instance) {
            if (paypalCheckoutErr) {
                console.error(paypalCheckoutErr);
                return;
            }

            paypal.Button.render({
                env: 'sandbox',
                style: {
                    color: 'gold',
                    shape: 'rect',
                    label: 'paypal',
                    tagline: false
                },

                payment: function () {
                    var amount = 0;
                    var prices = data('prices');
                    for (var key in prices) {
                        amount += $('#Quantities_' + key + '_').val() * prices[key];
                    }

                    return instance.createPayment({
                        flow: 'checkout',
                        intent: 'sale',
                        displayName: 'instantboxoffice',
                        amount: amount,
                        currency: 'USD'
                    });
                },

                onAuthorize: function (data, actions) {
                    return instance.tokenizePayment(data).then(function (payload) {
                        $('#nonce').val(payload.nonce);
                        $('#form').submit();
                    });
                },

                onCancel: function (data) {
                    console.log('cancelled', JSON.stringify(data, 0, 2));
                },

                onError: function (err) {
                    console.error(err);
                }
            }, '#paypal-button');
        });
    });
});

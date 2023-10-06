using System;
using System.Collections.Generic;
using System.Text;
using PayPal.Api;
using Services.Helper;

namespace Services
{
    public static class PayPalPaymentService
    {

        public static Plan CreatePlanObject(string planName, string planDescription, string returnUrl, string cancelUrl,
    string frequency, int frequencyInterval, decimal planPrice,
    decimal shippingAmount = 0, decimal taxPercentage = 0, bool trial = false, int trialLength = 0, decimal trialPrice = 0)
        {
            var apiContext = PayPalConfiguration.GetAPIContext();
            // Define the plan and attach the payment definitions and merchant preferences.
            // More Information: https://developer.paypal.com/docs/rest/api/payments.billing-plans/
            var plan= new Plan
            {
                name = planName,
                description = planDescription,
              //  type = PlanType.Fixed,

                // Define the merchant preferences.
                // More Information: https://developer.paypal.com/webapps/developer/docs/api/#merchantpreferences-object
                merchant_preferences = new MerchantPreferences()
                {
                    //  setup_fee = GetCurrency("1"),
                    return_url = returnUrl,
                    cancel_url = cancelUrl,
                    auto_bill_amount = "YES",
                    initial_fail_amount_action = "CONTINUE",
                    max_fail_attempts = "0"
                },
                payment_definitions = GetPaymentDefinitions(trial, trialLength, trialPrice, frequency, frequencyInterval, planPrice, shippingAmount, taxPercentage)
            };
           var createdPlan= plan.Create(apiContext);
            return createdPlan;
        }


        private static List<PaymentDefinition> GetPaymentDefinitions(bool trial, int trialLength, decimal trialPrice,
         string frequency, int frequencyInterval, decimal planPrice, decimal shippingAmount, decimal taxPercentage)
        {
            var paymentDefinitions = new List<PaymentDefinition>();

            if (trial)
            {
                // Define a trial plan that will charge 'trialPrice' for 'trialLength'
                // After that, the standard plan will take over.
                paymentDefinitions.Add(
                    new PaymentDefinition()
                    {
                        name = "Trial",
                        type = "TRIAL",
                        frequency = frequency,
                        frequency_interval = frequencyInterval.ToString(),
                        amount = GetCurrency(trialPrice.ToString()),
                        cycles = trialLength.ToString(),
                        charge_models = GetChargeModels(trialPrice, shippingAmount, taxPercentage)
                    });
            }

            // Define the standard payment plan. It will represent a 'frequency' (monthly, etc)
            // plan for 'planPrice' that charges 'planPrice' (once a month) for #cycles.
            var regularPayment = new PaymentDefinition
            {
                name = "Standard Plan",
                type = "REGULAR",
                frequency = frequency,
                frequency_interval = frequencyInterval.ToString(),
                amount = GetCurrency(planPrice.ToString()),
                // > NOTE: For `IFNINITE` type plans, `cycles` should be 0 for a `REGULAR` `PaymentDefinition` object.
                cycles = "11",
                charge_models = GetChargeModels(trialPrice, shippingAmount, taxPercentage)
            };
            paymentDefinitions.Add(regularPayment);

            return paymentDefinitions;
        }

        private static Currency GetCurrency(string amount)
        {
            var currency = new Currency
            {
                currency = "USD",
                value = amount
            };
            return currency;
        }

        private static List<ChargeModel> GetChargeModels(decimal planPrice, decimal shippingAmount, decimal taxPercentage)
        {
            // Create the Billing Plan
            var chargeModels = new List<ChargeModel>();
            if (shippingAmount > 0)
            {
                chargeModels.Add(new ChargeModel()
                {
                    type = "SHIPPING",
                    amount = GetCurrency(shippingAmount.ToString())
                });
            }
            if (taxPercentage > 0)
            {
                chargeModels.Add(new ChargeModel()
                {
                    type = "TAX",
                    amount = GetCurrency(String.Format("{0:f2}", planPrice * taxPercentage / 100))
                });
            }

            return chargeModels;
        }


        public static void UpdateBillingPlan(string planId, string path, object value)
        {
            // PayPal Authentication tokens
            var apiContext = PayPalConfiguration.GetAPIContext();

            // Retrieve Plan
            var plan = Plan.Get(apiContext, planId);

            // Activate the plan
            var patchRequest = new PatchRequest()
    {
        new Patch()
        {
            op = "replace",
            path = path,
            value = value
        }
    };
            plan.Update(apiContext, patchRequest);
        }



        public static Agreement CreateBillingAgreement(string planId, ShippingAddress shippingAddress,
    string name, string description, DateTime startDate)
        {
            // PayPal Authentication tokens
            var apiContext = PayPalConfiguration.GetAPIContext();

            var agreement = new Agreement()
            {
                name = name,
                description = description,
                start_date = startDate.ToString("yyyy-MM-ddTHH:mm:ss") + "Z",
                payer = new Payer() { payment_method = "paypal" },
                plan = new Plan() { id = planId },
                shipping_address = shippingAddress
            };

            var createdAgreement = agreement.Create(apiContext);
            return createdAgreement;
        }




        public static void ExecuteBillingAgreement(string token)
        {
            // PayPal Authentication tokens
            var apiContext = PayPalConfiguration.GetAPIContext();

            var agreement = new Agreement() { token = token };
            var executedAgreement = agreement.Execute(apiContext);
        }


        public static void SuspendBillingAgreement(string agreementId)
        {
            var apiContext = PayPalConfiguration.GetAPIContext();

            var agreement = new Agreement() { id = agreementId };
            agreement.Suspend(apiContext, new AgreementStateDescriptor()
            { note = "Suspending the agreement" });
        }


        public static void ReactivateBillingAgreement(string agreementId)
        {
            var apiContext = PayPalConfiguration.GetAPIContext();

            var agreement = new Agreement() { id = agreementId };
            agreement.ReActivate(apiContext, new AgreementStateDescriptor()
            { note = "Reactivating the agreement" });
        }



        public static void CancelBillingAgreement(string agreementId)
        {
            var apiContext = PayPalConfiguration.GetAPIContext();

            var agreement = new Agreement() { id = agreementId };
            agreement.Cancel(apiContext, new AgreementStateDescriptor()
            { note = "Cancelling the agreement" });
        }


        public static Payment CreatePayment(string baseUrl, string intent)
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            var apiContext = PayPalConfiguration.GetAPIContext();

            // Payment Resource
            var payment = new Payment()
            {
                intent = intent,    // `sale` or `authorize`
                payer = new Payer() { payment_method = "paypal" },
                transactions = GetTransactionsList(),
                redirect_urls = GetReturnUrls(baseUrl, intent)
            };


            // Create a payment using a valid APIContext
            var createdPayment = payment.Create(apiContext);

            return createdPayment;
        }


        private static List<Transaction> GetTransactionsList()
        {
            // A transaction defines the contract of a payment
            // what is the payment for and who is fulfilling it. 
            var transactionList = new List<Transaction>();

            // The Payment creation API requires a list of Transaction; 
            // add the created Transaction to a List
            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                //  invoice_number = Random(),
                amount = new Amount()
                {
                    currency = "USD",
                    total = "100.00",       // Total must be equal to sum of shipping, tax and subtotal.
                    details = new Details() // Details: Let's you specify details of a payment amount.
                    {
                        tax = "15",
                        shipping = "10",
                        subtotal = "75"
                    }
                },
                item_list = new ItemList()
                {
                    items = new List<Item>()
            {
                new Item()
                {
                    name = "Item Name",
                    currency = "USD",
                    price = "15",
                    quantity = "5",
                    sku = "sku"
                }
            }
                }
            });
            return transactionList;
        }

        private static RedirectUrls GetReturnUrls(string baseUrl, string intent)
        {
            var returnUrl = intent == "sale" ? "/Home/PaymentSuccessful" : "/Home/AuthorizeSuccessful";

            // Redirect URLS
            // These URLs will determine how the user is redirected from PayPal 
            // once they have either approved or canceled the payment.
            return new RedirectUrls()
            {
                cancel_url = baseUrl + "/Home/PaymentCancelled",
                return_url = baseUrl + returnUrl
            };
        }

        public static Payment ExecutePayment(string paymentId, string payerId)
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            var apiContext = PayPalConfiguration.GetAPIContext();

            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };

            // Execute the payment.
            var executedPayment = payment.Execute(apiContext, paymentExecution);

            return executedPayment;
        }
    }
}

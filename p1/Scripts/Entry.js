
$(document).ready(function () {
   
        $("#item_rate").val(0);
            $("#item_Master_item_unit").val("");
            $("#qty").val(1);

            $('#item_name').prop('selectedIndex', 0);


            $("#cancelAddVendor").on('click', function () {

        $("#VendorSelect").css("display", "flex");
                $("#VendorAdd").css("display", "none");
            });
            $("#cancelAddItem").on('click', function () {
        $("#ItemSelect").css("display", "flex");
                $("#ItemAdd").css("display", "none");

            });

            $("#vendor_no").append(new Option("New Vendor", -1));
            $("#item_name").append(new Option("New Item", -1));




            $("#vendorAdd_vendor_name").val('');
            $("#vendorAdd_vendor_contactno").val('');
            $("#vendorAdd_vendor_address").val('');
            $("#vendorAdd_vendor_state").val($("#target option:first").val());
            $("#vendorAdd_vendor_GSTno").val('');
            $("#itemAddModel_item_name").val('');
            $("#itemAddModel_item_name").val('');
            $("#itemAddModel_item_rate").val('');
            $("#itemAddModel_item_unit").val('');

            $("#finalAddVendor").on('click', function () {
                var flag = 0;


                $("#vendor_name_error").html("");
                $("#vendor_contactno_error").html("");
                $("#vendorAdd_vendor_GSTno").html("");
                $("#vendor_address_error").html("");
                $("#vendor_state_error").html("");

                var name = $("#vendorAdd_vendor_name");
                var no = $("#vendorAdd_vendor_contactno");
                var gstno = $("#vendorAdd_vendor_GSTno");
                var address = $("#vendorAdd_vendor_address");
                var state = $("#vendorAdd_vendor_state");
                if (name.val() == "") {
        $("#vendor_name_error").html("Name is required");
                    flag = 1;
                }
                else {
                    var patt1 = /^[A-Z][a-z]+\s[A-Z][a-z]+$/;
                    if (!patt1.test(name.val())) {
        $("#vendor_name_error").html("Name should be alphabetic");
                        flag = 1;
                    }
                }
                if (no.val() == "") {
        $("#vendor_contactno_error").html("Number is required");
                    flag = 1;
                }
                else {
                    var patt2 = /^([6-9][0-9]{9})+$/;
                    if (!patt2.test(no.val())) {
                        $("#vendor_contactno_error").html("Number must be digit and length of 10 and correct format");
                        flag = 1;
                    }
                }
                if (gstno.val() == "") {

        $("#vendor_GSTno_error").html("GST no is required");
                    flag = 1;
                }
                else {

        //12ABCDE1234A1Z3
        patt3 = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$/;
                    if (!patt3.test(gstno.val())) {
        $("#vendor_GSTno_error").html("Check GSTno");
                        flag = 1;
                    }

                }
                if (address.val() == "") {
        $("#vendor_address_error").html("Address is required");
                }
                if (state.val().length == 0) {
        $("#vendor_state_error").html("State is required");
                    flag = 1;
                }
                if (flag == 0) {
        $("#finalAddVendor").prop('type', 'submit');

                }



            });


            $("#finalAddItem").on('click', function () {
                var flag = 0;
                $("#item_name_error").html("");
                $("#item_rate_error").html("");
                $("#item_unit_error").html("");
                var name = $("#itemAddModel_item_name");
                var rate = $("#itemAddModel_item_rate");
                var unit = $("#itemAddModel_item_unit");
                if (name.val() == "") {
        $("#item_name_error").html("Item name is  required");
                    flag = 1;

                }
                if (rate.val() == "") {

        $("#item_rate_error").html("Item rate is required");
                    flag = 1
                }
                if (unit.val() == "") {

        $("#item_unit_error").html("Item unit is required");
                    flag = 1;

                }
                if (flag == 0) {
        $("#finalAddItem").prop('type', 'submit');

                }
            });



            $("#placeOrder").on('click', function () {
                var flag = 0;
                $("#invoice_no_error").html("");
                $("#invoice_date_error").html("");
                $("#vendor_error").html("");
                var order_date = $("#order_date");
                var invoice_no = $("#invoice_no");
              
                var invoice_date = $("#invoice_date");
                var vendor = $("#vendor_no");
                if (order_date.val() == "") {
                    $("#order_date_error").html("Order Date is required");
                    flag = 1;
                }
                else {
                    var now = new Date();
                    if (order_date.val() > now) {
                        $("#order_date_error").html("Check order date ");
                        flag = 1;
                    }
                }

                
                if (vendor.val().length == 0) {
        $("#vendor_error").html("Select Vendor");
                    var flag = 1;

                }
             
               
                if (flag == 0) {
        $("#placeOrder").prop('type', 'submit');

                }

            });


            $("#addItemList").on('click', function () {
                var flag = 0
                if ($("#item_name").val().length == 0) {
        flag = 1;
                }
                if (flag == 0) {
        $("#addItemList").prop('type', 'submit');


                }
            });


        

        });


        function clearError() {

        $("#vendor_name_error").html(" ");
            $("#vendor_contactno_error").html(" ");
            $("#vendor_GSTno_error").html(" ");
            $("#vendor_address_error").html(" ");
            $("#vendor_state_error").html(" ");
        }
   
$(document).ready(function() {
  $("#myform").jqxValidator({
    rules: [
      { input: "#username", message: "Username is required", action: "keyup, blur", rule: "required" },
      { input: "#password", message: "Password is required", action: "keyup, blur", rule: "required" }
    ],
    ajax: true,
    ajaxSettings: {
      url: "path/to/server",
      type: "post"
    },
    onSuccess: function (response) {
      if (response.status == "error") {
        $.each(response.errors, function (field, message) {
          $("#myform").jqxValidator("addError", field, message);
        });
      } else {
        // success
      }
    }
  });
});



In this example, we first initialize the jqxValidator plugin on the form with id="myform". The rules option defines the validation rules for the form fields. The ajax option is set to true to indicate that an AJAX request will be made to the server for validation. The ajaxSettings option specifies the URL and type of the request. The onSuccess option is called when the AJAX request is successful and receives the response from the server. If the response indicates an error, we use the $.each() function to loop through the response.errors object and add the error messages to the form fields using the jqxValidator("addError", field, message) method. If the response indicates success, we can perform any desired action.




$("#myform").jqxValidator({
  rules: [
    {
      input: "#username",
      message: "Username is required!",
      action: "keyup, blur",
      rule: "required"
    },
    // additional rules...
  ],
  ajax: true,
  submit: function() {
    $.ajax({
      url: "path/to/server",
      type: "post",
      data: $("#myform").serialize(),
      success: function(response) {
        if (response.status == "error") {
          $.each(response.errors, function(field, message) {
            $("#myform").jqxValidator("addRule", {
              input: "#" + field,
              message: message,
              action: "keyup, blur",
              rule: "required"
            });
          });
          $("#myform").jqxValidator("validate");
        } else {
          // success
        }
      }
    });
  }
});


In this example, we first initialize the jqxValidator plugin on the form with id="myform" and set the rules property to specify the validation rules for individual form fields. The ajax property is set to true to indicate that an AJAX request should be made to the server when the form is submitted. In the submit function, we make an AJAX request to the server and pass the form data using $("#myform").serialize(). If the response from the server indicates an error, we use the $.each() function to loop through the response.errors object and add error messages to the corresponding form fields using the jqxValidator("addRule", ...) method. If the response indicates success, we can perform any desired action.

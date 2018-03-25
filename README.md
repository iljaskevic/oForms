### oForms API documentation ###

## Get Started ##

To use, include the following in the `<head>` section of your site:
```
<script src="https://oformsapi.azurewebsites.net/js/v1/oforms.js"></script>
```
Then add the following script:
```
var oforms = new oForm("form-id", "api-key");
```
OR, you can add the hadler:
```
function successHandler(data) {
  ...
}
function errorHandler(error) {
  ...
}
var oforms = new oForm("form-id", "api-key", {
  success: successHandler,
  error: errorHandler
});
```

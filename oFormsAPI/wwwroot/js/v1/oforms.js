function oForm(formId, accessKey, callbacks) {
    console.log('Added oForms: ' + formId + "-" + accessKey);
    this.fId = formId;
    this.apiKey = accessKey;
    if (callbacks !== undefined) {
        this.onError = callbacks.error;
        this.onSuccess = callbacks.success;
    }

    let oFormsParent = this;

    $("#" + this.fId).submit(function (e) {
        e.preventDefault();
        var data = $("#" + oFormsParent.fId).serializeArray();

        var submission = {};
        for (var i = 0; i < data.length; i++) {
            submission[data[i]['name']] = data[i]['value'];
        }

        $.ajax({
            type: "POST",
            url: 'https://oformsapi.azurewebsites.net/api/v1/form',
            data: JSON.stringify(submission),
            contentType: 'application/json',
            headers: { 'api-key': oFormsParent.apiKey },
            dataType: 'json',
            success: oFormsParent.onSuccess,
            error: oFormsParent.onError
        });
    });
}

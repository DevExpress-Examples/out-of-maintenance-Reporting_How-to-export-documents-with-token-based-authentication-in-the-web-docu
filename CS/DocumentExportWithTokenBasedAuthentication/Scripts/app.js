function ReportViewerInit(s, e) {
    DevExpress.Reporting.Viewer.Settings.HandlerUri("/ReportViewer/Invoke");
    DevExpress.Reporting.Viewer.Settings.AsyncExportApproach(true);
}

function ViewModel() {
    var self = this;

    var tokenKey = 'accessToken';

    self.result = ko.observable();
    self.user = ko.observable();

    self.registerEmail = ko.observable();
    self.registerPassword = ko.observable();
    self.registerPassword2 = ko.observable();

    self.loginEmail = ko.observable();
    self.loginPassword = ko.observable();
    self.errors = ko.observableArray([]);

    function showError(jqXHR) {

        self.result(jqXHR.status + ': ' + jqXHR.statusText);

        var response = jqXHR.responseJSON;
        if (response) {
            if (response.Message) self.errors.push(response.Message);
            if (response.ModelState) {
                var modelState = response.ModelState;
                for (var prop in modelState) {
                    if (modelState.hasOwnProperty(prop)) {
                        var msgArr = modelState[prop]; // expect array here
                        if (msgArr.length) {
                            for (var i = 0; i < msgArr.length; ++i) self.errors.push(msgArr[i]);
                        }
                    }
                }
            }
            if (response.error) self.errors.push(response.error);
            if (response.error_description) self.errors.push(response.error_description);
        }
    }

    self.getAuthHeaders = function () {
        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        return headers;
    }

    self.callApi = function () {
        self.result('');
        self.errors.removeAll();

        $.ajax({
            type: 'GET',
            url: '/api/values',
            headers: self.getAuthHeaders()
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }

    self.openReport = function () {
        self.result('');
        self.errors.removeAll();

        DevExpress.Analytics.Utils.ajaxSetup.ajaxSettings = {
            headers: self.getAuthHeaders()
        };
        webDocumentViewer1.OpenReport("testReport");
    }

    self.register = function () {
        self.result('');
        self.errors.removeAll();

        var data = {
            Email: self.registerEmail(),
            Password: self.registerPassword(),
            ConfirmPassword: self.registerPassword2()
        };

        $.ajax({
            type: 'POST',
            url: '/api/Account/Register',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            self.result("Done!");
        }).fail(showError);
    }

    self.login = function () {
        self.result('');
        self.errors.removeAll();

        var loginData = {
            grant_type: 'password',
            username: self.loginEmail(),
            password: self.loginPassword()
        };

        $.ajax({
            type: 'POST',
            url: '/Token',
            data: loginData
        }).done(function (data) {
            self.user(data.userName);
            // Cache the access token in session storage.
            sessionStorage.setItem(tokenKey, data.access_token);
        }).fail(showError);
    }

    self.logout = function () {
        // Log out from the cookie based logon.
        $.ajax({
            type: 'POST',
            url: '/api/Account/Logout',
            headers: self.getAuthHeaders()
        }).done(function (data) {
            // Successfully logged out. Delete the token.
            self.user('');
            sessionStorage.removeItem(tokenKey);
        }).fail(showError);
    }
}

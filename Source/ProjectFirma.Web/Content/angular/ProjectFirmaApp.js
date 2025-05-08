(function () {
    "use strict";
    // create the angular app
    angular.module("ProjectFirmaApp", [
        "ng-currency",
        "server-validate",
        "ui.bootstrap"
    ]);

    angular.module("ProjectFirmaApp").filter("nfcurrency", ["$filter", "$locale", function ($filter, $locale) {
        var currency = $filter("currency"), formats = $locale.NUMBER_FORMATS;
        return function (amount, symbol) {
            var value = currency(amount, symbol);
            if (Sitka.Methods.isUndefinedNullOrEmpty(value))
            {
                return value;
            }
            return value.replace(new RegExp("\\" + formats.DECIMAL_SEP + "\\d{2}"), "");
        };
    }]);


    // The following code is modified from an answer on stackoverflow (https://stackoverflow.com/a/31644349) for creating 
    // a multi - file input angularjs directive. Used initially for [WADNR-1626]
    angular.module("ProjectFirmaApp").directive('ngFileModel', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var model = $parse(attrs.ngFileModel);
                var isMultiple = attrs.multiple;
                var modelSetter = model.assign;
                element.bind('change', function () {
                    var values = [];
                    angular.forEach(element[0].files, function (item) {
                        var value = {
                            // File Name 
                            name: item.name,
                            // File Size 
                            size: item.size,
                            // File URL to view 
                            url: URL.createObjectURL(item),
                            // File Input Value 
                            _file: item
                        };
                        values.push(value);
                    });
                    scope.$apply(function () {
                        if (isMultiple) {
                            modelSetter(scope, values);
                        } else {
                            modelSetter(scope, values[0]);
                        }
                    });
                });

                var highlightEvents = ['dragenter', 'dragover'];
                highlightEvents.forEach(eventName => {
                    element.bind(eventName, highlight);
                });
                var unhighlightEvents = ['dragleave', 'drop'];
                unhighlightEvents.forEach(eventName => {
                    element.bind(eventName, unhighlight);
                });

                function highlight(e) {
                    element.addClass('highlight');
                }
                function unhighlight(e) {
                    element.removeClass('highlight');
                }
            }
        };
    }]);

}());


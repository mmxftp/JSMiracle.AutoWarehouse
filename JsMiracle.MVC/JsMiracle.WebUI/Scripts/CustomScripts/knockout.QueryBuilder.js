
//外部必须定义变量“exports.fieldsJson” （JSON数组）
//格式：
    //[{
    //    'fieldText':'显示名称',
    //    'fieldValue':'数值',
    //    'isString': 是否字符串 true / false
//}]
//External variables must be defined "exports.fieldsJson" (JSON array)
//Format: 
//[{
//    'fieldText': 'Display Name',
//    'fieldValue': 'value',
//    'isString': true / false
//}]
window.QueryBuilder = (function (exports, ko) {

    exports.fieldsJson = [];

    function searchFieldVaule(fieldName) {
        var d;
        $.each(exports.fieldsJson, function (index, element) {
            if (element.fieldValue == fieldName) {
                d = element;
                return;
            }
        });
        return d;
    }

    exports.RefreshGroup = null;

    exports.Condition = function (fieldCondition) {
        var self = this;

        self.templateName = 'condition-template';
        self.fields = ko.observableArray(exports.fieldsJson);
        self.selectedField = ko.observable(0);

        self.comparisons = ko.observableArray(['=', '<>', '<', '<=', '>', '>=']);

        self.selectedComparison = ko.observable('=');
        self.value = ko.observable(0);

        if (fieldCondition) {

            var d = searchFieldVaule(fieldCondition.FieldName);

            self.selectedField(d);
            self.selectedComparison(fieldCondition.FieldOP);
            self.value(fieldCondition.FieldValue);
        }

        // the text() function is just an example to show output
        self.text = ko.computed(function () {
            return self.selectedField().fieldValue +
                  ' ' +
                  self.selectedComparison() +
                  ' ' +
                  (self.selectedField().isString ? '"' : '')
                  +
                  self.value()
                  +
                  (self.selectedField().isString ? '"' : '');
        });

        self.fieldsCondition = ko.computed(function () {
            return {
                'FieldName': self.selectedField().fieldValue,
                'FieldOP': self.selectedComparison(),
                'FieldValue': self.value(),
            };
        });
    }

    exports.Group = function (groupCondition) {
        var self = this;

        self.templateName = 'group-template';
        self.children = ko.observableArray();
        self.logicalOperators = ko.observableArray(['AND', 'OR']);
        self.selectedLogicalOperator = ko.observable('AND');

        self.addCondition = function () {
            self.children.push(new exports.Condition());
        };

        self.addGroup = function () {
            self.children.push(new exports.Group());
            //console.log(exports.RefreshGroup);
            if (jQuery.isFunction(exports.RefreshGroup)) {
                exports.RefreshGroup();
            }
        };

        self.removeChild = function (child) {
            self.children.remove(child);
        };

        // the text() function is just an example to show output
        self.text = ko.computed(function () {
            var result = '(';
            var op = '';
            for (var i = 0; i < self.children().length; i++) {
                var child = self.children()[i];
                result += op + child.text();
                op = ' ' + self.selectedLogicalOperator() + ' ';
            }
            return result += ')';
        });

        self.fieldsCondition = ko.computed(function () {

            var fieldsConditionList = new Array();
            var groupConditionList = new Array();
            for (var i = 0; i < self.children().length; i++) {
                var child = self.children()[i];

                if (child.templateName == 'condition-template')
                    fieldsConditionList.push(child.fieldsCondition());
                else
                    groupConditionList.push(child.fieldsCondition());
            }

            return {
                'GroupOP': self.selectedLogicalOperator(),
                'FieldsList': fieldsConditionList,
                'GroupList': groupConditionList
            }
        });

        if (groupCondition && groupCondition.FieldsList) {
            self.selectedLogicalOperator(groupCondition.GroupOP);

            if (groupCondition.FieldsList.length > 0) {
                for (var j = 0; j < groupCondition.FieldsList.length; j++) {
                    var field = groupCondition.FieldsList[j];
                    var cond = new exports.Condition(field);
                    self.children.push(cond);
                }
            }

            if (groupCondition.GroupList.length > 0) {
                for (var g = 0 ; g < groupCondition.GroupList.length; g++) {
                    var group = groupCondition.GroupList[g];
                    var gp = new exports.Group(group);
                    self.children.push(gp);
                }
            }
        }
        else {
            // give the group a single default condition
            self.children.push(new exports.Condition());
        }

    }

    exports.ViewModel = function (express) {
        var self = this;

        self.group = ko.observable(new exports.Group(express));

        // the text() function is just an example to show output
        self.text = ko.computed(function () {
            return self.group().text();
        });

        self.Express = ko.computed(function () {
            return ko.toJSON(self.group().fieldsCondition());
        });
    }

    return exports;

})(window.QueryBuilder || {}, window.ko);
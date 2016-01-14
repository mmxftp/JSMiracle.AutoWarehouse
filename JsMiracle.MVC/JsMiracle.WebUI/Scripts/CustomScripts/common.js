
var JsMiracleCommon = {

    // 用于easyui datagrid 得到后台传送的日期格式化方法
    dateTimeFormatter: function (value, row, index) {
        //console.log(value);
        if (value == null)
            return '';

        var x = moment(value).format('YYYY-MM-DD HH:mm:ss')
        return x;
        //if (value == null)
        //    return '';

        //var obj = eval('(' + " new " + value.substr(1, value.length - 2) + ')');
        //return moment(obj).format('YYYY-MM-DD HH:mm:ss')
    },

    // 弹出浮层 名称
    layerWindowName: 'floatingLayerWindow',

    cancelWidow: function (formID) {
        $('#' + this.layerWindowName).window('close');
        //this.closeWindow();
        //$('#' + formID).form('submit');
    },

    // 关闭当前子窗体, 并刷新父窗体
    closeWindow: function () {
        //parent.$('#' + layerWindowName).window('close');
        $('#' + this.layerWindowName).window('close');
        var fun = $('#' + this.layerWindowName).window('options').queryParams;
        //console.log(fun);

        if (this.isFunction(fun)) {
            // 得到除第2个参数后的所有参数 (索引从0开始计数,1为第2个)
            //var args = Array.prototype.slice.call(arguments, 1);
            var args = Array.prototype.slice.call(arguments);
            //console.log(args);
            fun(args);
        }
    },


    // 加入需显示的内容，并打开此层
    showWindow: function (title, url, width, height, fun, modal, minimizable, maximizable) {
        //console.log(fun);
        $('#' + this.layerWindowName).window(
        {
            // 自定义函数
            queryParams: fun,

            title: title,
            width: width,
            height: height,
            iconCls: 'icon-edit',
            href: url,
            //content: '<iframe scrolling="yes" frameborder="0"  src="'
            //        + url
            //        + '" style="width:98%;height:98%;" ></iframe>',

            modal: modal === undefined ? true : modal,
            minimizable: minimizable === undefined ? false
                    : minimizable,
            maximizable: maximizable === undefined ? false
                    : maximizable,
            collapsible: false,
            closed: true,
            //resizable:false,
            loadingMessage: '正在加载数据，请稍等片刻......'
        });
        $('#' + this.layerWindowName).window('open');
    },

    // 加入需显示的内容，并打开此层
    showWindowContent: function (title, content, width, height, iconCls) {
        $('#' + this.layerWindowName).window(
        {
            title: title,
            width: width,
            height: height,
            iconCls: iconCls === undefined ? 'icon-no' :iconCls,
            content: content,
            modal: true ,
            collapsible: false,
            closed: true,
            resizable:true,
            loadingMessage: '正在加载数据，请稍等片刻......'
        });
        $('#' + this.layerWindowName).window('open');
    },

    submit:function(btn, frmid){
        $(btn).linkbutton('disable');
        $("#"+frmid).form('submit');
    },

    //判断是否为数组
    isArray: function (source) {
        return '[object Array]' == Object.prototype.toString.call(source);
    },
    //判断是否为日期对象
    isDate: function (o) {
        // return o instanceof Date;
        return {}.toString.call(o) === "[object Date]" && o.toString() !== 'Invalid Date' && !isNaN(o);
    },
    //判断是否为Element对象
    isElement: function (source) {
        return !!(source && source.nodeName && source.nodeType == 1);
    },
    //判断目标参数是否为function或Function实例
    isFunction: function (source) {
        // chrome下,'function' == typeof /a/ 为true.
        return '[object Function]' == Object.prototype.toString.call(source);
    },
    //判断目标参数是否number类型或Number对象
    isNumber: function (source) {
        return '[object Number]' == Object.prototype.toString.call(source) && isFinite(source);
    },
    //判断目标参数是否为Object对象
    isObject: function (source) {
        return 'function' == typeof source || !!(source && 'object' == typeof source);
    },
    //判断目标参数是否string类型或String对象
    isString: function (source) {
        return '[object String]' == Object.prototype.toString.call(source);
    },
    //判断目标参数是否Boolean对象
    isBoolean: function (o) {
        return typeof o === 'boolean';
    },

    // 删除指定对象
    removeItem: function (id, name, postUrl, successFunction) {
        $.messager.confirm('Confirm', '确定删除\'' + name + '\'?'
            , function (r) {
                if (r) {
                    $.post(postUrl, { "id": id }, function (result) {
                        if (result.success) {
                            if (successFunction && JsMiracleCommon.isFunction(successFunction))
                                successFunction(result);
                            //$('#' + refreshgrdid).datagrid('reload');    // 重新加载用户数据
                        } else {
                            $.messager.alert({    // 显示错误信息
                                title: 'Error',
                                msg: result.msg
                            });
                        }
                    }, 'json');
                }
            });
    },
}

// 页面初始化时加入新的用于显示浮动页面的层。
$(function () {
    $('body').append('<div id="' + JsMiracleCommon.layerWindowName + '" closed="true"></div>');
})

// 回车代替Tab 
// 用法 : $("#from").enterAsTab({ 'allowSubmit': true });
$.fn.enterAsTab = function (options) {
    var settings = $.extend({
        'allowSubmit': false
    }, options);
    this.find(':input:visible:not(disabled):not([readonly]),.easyui-linkbutton,select:visible').on("keypress", null, { localSettings: settings }, function (event) {
        if (settings.allowSubmit) {
            var type = $(this).attr("iconcls");
            if (type == "icon-save") {
                //console.log('save');
                return true;
            }
        }
        if (event.keyCode == 13) {
            console.log($(this));
            var elements = $(this).parents("form").eq(0).find(":input:visible:not(disabled):not([readonly]),.easyui-linkbutton,select:visible");
            var idx = elements.index(this);
            if (idx == elements.length - 1) {
                idx = -1;
            } else {
                //console.log(inputs[idx + 1]);
                elements[idx + 1].focus(); // handles submit buttons
            }
            try {
                if (elements[idx + 1].tagName == "INPUT")
                   elements[idx + 1].select();
            }
            catch (err) {
                console.error(err);
                // handle objects not offering select
            }
            return false;
        }
    });
    return this;
};

$.fn.firstInputFocus = function () {

    // 得到第一个input控件,焦点定位
    var input = this.find(':input:visible:not(disabled):not([readonly]):first');

    if (input) {
        input.focus();

        try {
            input.select();
        }
        catch (err) {
            console.error(err);
        }
    }
};


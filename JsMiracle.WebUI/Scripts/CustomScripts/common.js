
// 用于easyui datagrid 得到后台传送的日期格式化方法
function dateTimeFormatter(value, row, index) {
    if (value == null)
        return '';

    var obj = eval('(' + " new " + value.substr(1, value.length - 2) + ')');
    return moment(obj).format('YYYY-MM-DD HH:mm:ss')
}


// 弹出浮层
var layerWindowName = 'floatingLayerWindow';

// 关闭当前子窗体, 并刷新父窗体
function closeWindow() {
    //parent.$('#' + layerWindowName).window('close');
    $('#' + layerWindowName).window('close');
    var fun = $('#' + layerWindowName).window('options').queryParams;
    //console.log(fun);

    if ( isFunction(fun) ) {

        // 得到除第2个参数后的所有参数 (索引从0开始计数,1为第2个)
        //var args = Array.prototype.slice.call(arguments, 1);
        var args = Array.prototype.slice.call(arguments);
        //console.log(args);
        fun(args);
        //console.log(script);
        //// 在父窗体执行 脚本
        //parent.eval(script);
    }
}

// 页面初始化时加入新的用于显示浮动页面的层。
$(function () {
    $('body').append('<div id="' + layerWindowName + '" closed="true"></div>')
})

// 加入需显示的内容，并打开此层
function showWindow(title, href, width, height, fun, modal, minimizable, maximizable) {
    //console.log(fun);
    $('#'+layerWindowName).window(
    {
        // 自定义函数
        queryParams: fun,

        title: title,
        width: width,
        height: height,
        iconCls: 'icon-edit',
        href: href,
        //content: '<iframe scrolling="yes" frameborder="0"  src="'
        //        + href
        //        + '" style="width:98%;height:98%;" ></iframe>',

        modal: modal === undefined ? true : modal,
        minimizable: minimizable === undefined ? false
                : minimizable,
        maximizable: maximizable === undefined ? false
                : maximizable,
        collapsible:false,
        closed: true,
        //resizable:false,
        loadingMessage: '正在加载数据，请稍等片刻......'
    });
    $('#' + layerWindowName).window('open');
}

// 回车代替Tab 
// 用法 : $("#from").enterAsTab({ 'allowSubmit': true });
$.fn.enterAsTab = function (options) {
    var settings = $.extend({
        'allowSubmit': false
    }, options);
    this.find('input, select').on("keypress", null, { localSettings: settings }, function (event) {
        if (settings.allowSubmit) {
            var type = $(this).attr("type");
            if (type == "submit") {
                return true;
            }
        }
        if (event.keyCode == 13) {

            var inputs = $(this).parents("form").eq(0).find(":input:visible:not(disabled):not([readonly])");
            var idx = inputs.index(this);
            if (idx == inputs.length - 1) {
                idx = -1;
            } else {
                inputs[idx + 1].focus(); // handles submit buttons
            }
            try {
                inputs[idx + 1].select();
            }
            catch (err) {
                // handle objects not offering select
            }
            return false;
        }
    });
    return this;
};


//判断是否为数组
isArray = function (source) {
    return '[object Array]' == Object.prototype.toString.call(source);
};
//判断是否为日期对象
isDate = function (o) {
    // return o instanceof Date;
    return {}.toString.call(o) === "[object Date]" && o.toString() !== 'Invalid Date' && !isNaN(o);
};
//判断是否为Element对象
isElement = function (source) {
    return !!(source && source.nodeName && source.nodeType == 1);
};
//判断目标参数是否为function或Function实例
isFunction = function (source) {
    // chrome下,'function' == typeof /a/ 为true.
    return '[object Function]' == Object.prototype.toString.call(source);
};
//判断目标参数是否number类型或Number对象
isNumber = function (source) {
    return '[object Number]' == Object.prototype.toString.call(source) && isFinite(source);
};
//判断目标参数是否为Object对象
isObject = function (source) {
    return 'function' == typeof source || !!(source && 'object' == typeof source);
};
//判断目标参数是否string类型或String对象
isString = function (source) {
    return '[object String]' == Object.prototype.toString.call(source);
};
//判断目标参数是否Boolean对象
isBoolean = function (o) {
    return typeof o === 'boolean';
};
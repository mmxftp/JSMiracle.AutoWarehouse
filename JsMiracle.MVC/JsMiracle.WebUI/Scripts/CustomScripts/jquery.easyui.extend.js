﻿/**
 * @author {CaoGuangHui}
 */
$.extend($.fn.tabs.methods, {
    getTabById: function (jq, id) {
        var tabs = $.data(jq[0], 'tabs').tabs;
        for (var i = 0; i < tabs.length; i++) {
            var tab = tabs[i];
            if (tab.panel('options').id == id) {
                return tab;
            }
        }
        return null;
    },
    selectById: function (jq, id) {
        return jq.each(function () {
            var state = $.data(this, 'tabs');
            var opts = state.options;
            var tabs = state.tabs;
            var selectHis = state.selectHis;
            if (tabs.length == 0) { return; }
            var panel = $(this).tabs('getTabById', id); // get the panel to be activated 
            if (!panel) { return }
            var selected = $(this).tabs('getSelected');
            if (selected) {
                if (panel[0] == selected[0]) { return }
                $(this).tabs('unselect', $(this).tabs('getTabIndex', selected));
                if (!selected.panel('options').closed) { return }
            }
            panel.panel('open');
            var title = panel.panel('options').title;        // the panel title 
            selectHis.push(title);        // push select history 
            var tab = panel.panel('options').tab;        // get the tab object 
            tab.addClass('tabs-selected');
            // scroll the tab to center position if required. 
            var wrap = $(this).find('>div.tabs-header>div.tabs-wrap');
            var left = tab.position().left;
            var right = left + tab.outerWidth();
            if (left < 0 || right > wrap.width()) {
                var deltaX = left - (wrap.width() - tab.width()) / 2;
                $(this).tabs('scrollBy', deltaX);
            } else {
                $(this).tabs('scrollBy', 0);
            }
            $(this).tabs('resize');
            opts.onSelect.call(this, title, $(this).tabs('getTabIndex', panel));
        });
    },
    existsById: function (jq, id) {
        return $(jq[0]).tabs('getTabById', id) != null;
    }
});



// 增加验证方法
$.extend($.fn.validatebox.defaults.rules, {
    minLength: {
        validator: function (value, param) {
            return value.length == param[0];
        },
        message: '此框内容长度必须等于{0}'
    },

    valueNotEqual: {
        validator: function (value, param) {
            //console.log(value + ',' + param[0] + ',' + (value == param[0]));
            return value != param[0];
        },
        message: '请从下拉列表中选择数据'
    },

    /*必须和某个字段相等*/
    equalTo: {
        validator: function (value, param) {
            return $(param[0]).val() == value;
        },
        message: '字段不匹配'
    },

    equalMoreThan: {
        validator: function (value, param) {
            return parseFloat(value) > parseFloat(param);
        },
        message: '值必须大于 {0}'
    }
});



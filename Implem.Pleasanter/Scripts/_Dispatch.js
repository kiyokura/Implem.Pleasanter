﻿$p.setByJson = function (json, data, $control) {
    if (json) {
        $.each(json, function () {
            $p.setByJsonElement(this, data, $control);
        });
    }
    if (json.filter(function (d) {
        return d.Method === 'Html' ||
            d.Method === 'ReplaceAll' ||
            d.Method === 'Append' ||
            d.Method === 'Prepend';
    }).length > 0) {
        $p.apply();
        $p.applyValidator();
    }
}

$p.setByJsonElement = function (jsonElement, data, $control) {
    var method = jsonElement.Method;
    var target = jsonElement.Target;
    var value = jsonElement.Value;
    switch (method) {
        case 'Html':
            $(target).html(value);
            break;
        case 'ReplaceAll':
            $(value).replaceAll(target);
            break;
        case 'Message':
            $p.setMessage(value);
            break;
        case 'Href':
            location.href = value;
            break;
        case 'PushState':
            history.pushState(target, '', value);
            break;
        case 'SetData':
            $p.setData($(target));
            break;
        case 'SetFormData':
            data[target] = value;
            break;
        case 'Append':
            $(target).append(value);
            break;
        case 'Prepend':
            $(target).prepend(value);
            break;
        case 'After':
            if ($(target).length !== 0) {
                $(target).after(value);
            } else {
                $control.after(value);
            }
            break;
        case 'Before':
            if ($(target).length !== 0) {
                $(target).before(value);
            } else {
                $control.before(value);
            }
            break;
        case 'Remove':
            $(target).remove();
            break;
        case 'Focus':
            if (target === '') {
                $('#' + data.ControlId).focus();
            } else {
                $(target).focus();
            }
            break;
        case 'SetValue':
            $p.setValue($(target), value);
            break;
        case 'ClearFormData':
            $p.clearData(target, data, value);
            break;
        case 'CloseDialog':
            $('.ui-dialog-content').dialog('close');
            break;
        case 'Trigger':
            $(target).trigger(value);
            break;
        case 'Invoke':
            $p[target]();
        case 'WindowScrollTop':
            $(window).scrollTop(value);
            break;
        case 'FocusMainForm':
            $p.focusMainForm();
            break;
        case 'Empty':
            $(target).empty();
            break;
        case 'Disabled':
            $(target).prop('disabled', value);
            break;
    }
}
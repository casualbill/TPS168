﻿/*
Copyright 2011, KISSY UI Library v1.1.8dev
MIT Licensed
build time: ${build.time}
*/
KISSY.add("uibase", function (c) {
    function e(g) { a.apply(this, arguments); for (var h = this.constructor; h; ) { if (g && g[b] && h.HTML_PARSER) if (g[b] = c.one(g[b])) { var i = g[b], j = h.HTML_PARSER, l = void 0, m = void 0; for (l in j) if (j.hasOwnProperty(l)) { m = j[l]; if (c.isFunction(m)) this.__set(l, m.call(this, i)); else if (c.isString(m)) this.__set(l, i.one(m)); else c.isArray(m) && m[0] && this.__set(l, i.all(m[0])) } } h = h.superclass && h.superclass.constructor } f(this, "initializer", "constructor"); g && g.autoRender && this.render() } function f(g, h,
i) { for (var j = g.constructor, l = [], m, n, p; j; ) { p = []; if (n = j.__ks_exts) for (var o = 0; o < n.length; o++) if (m = n[o]) { if (i != "constructor") m = n[o].prototype[i]; m && p.push(m) } if (m = j.prototype[h]) p.push(m); p.length && l.push.apply(l, p.reverse()); j = j.superclass && j.superclass.constructor } for (o = l.length - 1; o >= 0; o--) l[o] && l[o].call(g) } var b = "srcNode", a = c.Base, d = c.Attribute.__capitalFirst, k = function () { }; e.HTML_PARSER = {}; e.ATTRS = { render: { valueFn: function () { return document.body } }, rendered: { value: false} }; c.extend(e, a, { render: function () {
    if (!this.get("rendered")) {
        this._renderUI();
        this.fire("renderUI"); f(this, "renderUI", "__renderUI"); this._bindUI(); this.fire("bindUI"); f(this, "bindUI", "__bindUI"); this._syncUI(); this.fire("syncUI"); f(this, "syncUI", "__syncUI"); this.set("rendered", true)
    } 
}, _renderUI: k, renderUI: k, _bindUI: function () { var g = this, h = g.__getDefAttrs(), i, j; for (i in h) if (h.hasOwnProperty(i)) { j = "_uiSet" + d(i); g[j] && function (l, m) { g.on("after" + d(l) + "Change", function (n) { g[m](n.newVal, n) }) } (i, j) } }, bindUI: k, _syncUI: function () {
    var g = this.__getDefAttrs(), h; for (h in g) if (g.hasOwnProperty(h)) {
        var i =
"_uiSet" + d(h); this[i] && this[i](this.get(h))
    } 
}, syncUI: k, destroy: function () { for (var g = this.constructor, h, i, j; g; ) { (i = g.prototype.destructor) && i.apply(this); if (h = g.__ks_exts) for (j = h.length - 1; j >= 0; j--) (i = h[j] && h[j].prototype.__destructor) && i.apply(this); g = g.superclass && g.superclass.constructor } this.fire("destroy"); this.detach() } 
}); e.create = function (g, h, i, j) {
    function l() { e.apply(this, arguments) } if (c.isArray(g)) { j = i; i = h; h = g; g = e } g = g || e; c.extend(l, g, i, j); if (h) {
        l.__ks_exts = h; c.each(h, function (m) {
            if (m) {
                c.each(["ATTRS",
"HTML_PARSER"], function (n) { if (m[n]) { l[n] = l[n] || {}; c.mix(l[n], m[n], false) } }); c.augment(l, m, false)
            } 
        })
    } return l
}; c.UIBase = e
}, { requires: ["core"] });
KISSY.add("uibase-align", function (c) {
    function e() { } function f(a, d) { var k = d.charAt(0), g = d.charAt(1), h, i, j, l; if (a) { a = c.one(a); h = a.offset(); i = a[0].offsetWidth; j = a[0].offsetHeight } else { h = { left: b.scrollLeft(), top: b.scrollTop() }; i = b.viewportWidth(); j = b.viewportHeight() } l = h.left; h = h.top; if (k === "c") h += j / 2; else if (k === "b") h += j; if (g === "c") l += i / 2; else if (g === "r") l += i; return { left: l, top: h} } var b = c.DOM; c.mix(e, { TL: "tl", TC: "tc", TR: "tr", CL: "cl", CC: "cc", CR: "cr", BL: "bl", BC: "bc", BR: "br" }); e.ATTRS = { align: {} }; e.prototype =
{ _uiSetAlign: function (a) { c.isPlainObject(a) && this.align(a.node, a.points, a.offset) }, align: function (a, d, k) { var g, h = this.get("el"); k = k || [0, 0]; g = h.offset(); a = f(a, d[0]); d = f(h, d[1]); d = [d.left - a.left, d.top - a.top]; g = [g.left - d[0] + +k[0], g.top - d[1] + +k[1]]; this.set("xy", g) }, center: function (a) { this.set("align", { node: a, points: [e.CC, e.CC], offset: [0, 0] }) } }; c.UIBase.Align = e
}, { host: "uibase" });
KISSY.add("uibase-box", function (c) {
    function e() { } c.namespace("UIBase"); var f = c.Node; c.mix(e, { APPEND: 1, INSERT: 0 }); e.ATTRS = { el: { setter: function (b) { if (c.isString(b)) return c.one(b) } }, elCls: {}, elStyle: {}, width: {}, height: {}, elTagName: { value: "div" }, elAttrs: {}, elOrder: { value: 1 }, html: { value: false} }; e.HTML_PARSER = { el: function (b) { return b } }; e.prototype = { __syncUI: function () { }, __bindUI: function () { }, __renderUI: function () {
        var b = this.get("render"), a = this.get("el"); b = new f(b); if (!a) {
            a = new f("<" + this.get("elTagName") +
">"); this.get("elOrder") ? b.append(a) : b.prepend(a); this.set("el", a)
        } 
    }, _uiSetElAttrs: function (b) { b && this.get("el").attr(b) }, _uiSetElCls: function (b) { b && this.get("el").addClass(b) }, _uiSetElStyle: function (b) { b && this.get("el").css(b) }, _uiSetWidth: function (b) { b && this.get("el").width(b) }, _uiSetHeight: function (b) { b && this.get("el").height(b) }, _uiSetHtml: function (b) { b !== false && this.get("el").html(b) }, __destructor: function () { var b = this.get("el"); if (b) { b.detach(); b.remove() } } 
    }; c.UIBase.Box = e
}, { host: "uibase" });
KISSY.add("uibase-close", function (c) {
    function e() { } c.namespace("UIBase"); var f = c.Node; e.ATTRS = { closable: { value: true }, closeBtn: {} }; e.HTML_PARSER = { closeBtn: ".ks-ext-close" }; e.prototype = { __syncUI: function () { }, _uiSetClosable: function (b) { var a = this.get("closeBtn"); if (a) b ? a.show() : a.hide() }, __renderUI: function () { var b = this.get("closeBtn"), a = this.get("contentEl"); if (!b && a) { b = (new f("<a href='#' class='ks-ext-close'><span class='ks-ext-close-x'>X</span></a>")).appendTo(a); this.set("closeBtn", b) } }, __bindUI: function () {
        var b =
this, a = b.get("closeBtn"); a && a.on("click", function (d) { b.hide(); d.halt() })
    }, __destructor: function () { var b = this.get("closeBtn"); b && b.detach() } 
    }; c.UIBase.Close = e
}, { host: "uibase" });
KISSY.add("uibase-constrain", function (c) {
    function e() { } function f(a) { var d; if (!a) return d; var k = this.get("el"); if (a !== true) { a = c.one(a); d = a.offset(); c.mix(d, { maxLeft: d.left + a[0].offsetWidth - k[0].offsetWidth, maxTop: d.top + a[0].offsetHeight - k[0].offsetHeight }) } else { a = document.documentElement.clientWidth; d = { left: b.scrollLeft(), top: b.scrollTop() }; c.mix(d, { maxLeft: d.left + a - k[0].offsetWidth, maxTop: d.top + b.viewportHeight() - k[0].offsetHeight }) } return d } c.namespace("UIBase"); var b = c.DOM; e.ATTRS = { constrain: { value: false} };
    e.prototype = { __bindUI: function () { }, __renderUI: function () { var a = this, d = a.__getDefAttrs(), k = d.x; d = d.y; var g = k.setter, h = d.setter; k.setter = function (i) { var j = g && g(i); if (j === undefined) j = i; if (!a.get("constrain")) return j; i = f.call(a, a.get("constrain")); return Math.min(Math.max(j, i.left), i.maxLeft) }; d.setter = function (i) { var j = h && h(i); if (j === undefined) j = i; if (!a.get("constrain")) return j; i = f.call(a, a.get("constrain")); return Math.min(Math.max(j, i.top), i.maxTop) }; a.addAttr("x", k); a.addAttr("y", d) }, __syncUI: function () { },
        __destructor: function () { } 
    }; c.UIBase.Constrain = e
}, { host: "uibase" });
KISSY.add("uibase-contentbox", function (c) {
    function e() { } c.namespace("UIBase"); var f = c.Node; e.ATTRS = { contentEl: {}, contentElAttrs: {}, contentTagName: { value: "div" }, content: {} }; e.HTML_PARSER = { contentEl: ".ks-contentbox" }; e.prototype = { __syncUI: function () { }, __bindUI: function () { }, __renderUI: function () {
        var b = this.get("contentEl"), a = this.get("el"); if (!b) {
            var d = c.makeArray(a[0].childNodes); b = (new f("<" + this.get("contentTagName") + " class='ks-contentbox'>")).appendTo(a); for (a = 0; a < d.length; a++) b.append(d[a]); this.set("contentEl",
b)
        } 
    }, _uiSetContentElAttrs: function (b) { b && this.get("contentEl").attr(b) }, _uiSetContent: function (b) { if (b !== undefined) if (c.isString(b)) this.get("contentEl").html(b); else { this.get("contentEl").html(""); this.get("contentEl").append(b) } }, __destructor: function () { } 
    }; c.UIBase.ContentBox = e
}, { host: "uibase" });
KISSY.add("uibase-drag", function (c) {
    function e() { } c.namespace("UIBase"); e.ATTRS = { handlers: { value: [] }, draggable: { value: true} }; e.prototype = { _uiSetHandlers: function (f) { f && f.length > 0 && this.__drag && this.__drag.set("handlers", f) }, __syncUI: function () { }, __renderUI: function () { }, __bindUI: function () { var f = this.get("el"); if (this.get("draggable") && c.Draggable) this.__drag = new c.Draggable({ node: f, handlers: this.get("handlers") }) }, _uiSetDraggable: function (f) {
        var b = this.__drag; if (b) if (f) {
            b.detach("drag"); b.on("drag",
this._dragExtAction, this)
        } else b.detach("drag")
    }, _dragExtAction: function (f) { this.set("xy", [f.left, f.top]) }, __destructor: function () { var f = this.__drag; f && f.destroy() } 
    }; c.UIBase.Drag = e
}, { host: "uibase" });
KISSY.add("uibase-loading", function (c) { function e() { } c.namespace("UIBase"); e.prototype = { loading: function () { if (!this._loadingExtEl) this._loadingExtEl = (new c.Node("<div class='ks-ext-loading' style='position: absolute;border: none;width: 100%;top: 0;left: 0;z-index: 99999;height:100%;*height: expression(this.parentNode.offsetHeight);'>")).appendTo(this.get("el")); this._loadingExtEl.show() }, unloading: function () { var f = this._loadingExtEl; f && f.hide() } }; c.UIBase.Loading = e }, { host: "uibase" });
KISSY.add("uibase-mask", function (c) {
    function e() { } c.namespace("UIBase"); var f, b = c.UA, a = 0; e.ATTRS = { mask: { value: false} }; e.prototype = { __bindUI: function () { }, __renderUI: function () { }, __syncUI: function () { }, _uiSetMask: function (d) { if (d) { this.on("show", this._maskExtShow); this.on("hide", this._maskExtHide) } else { this.detach("show", this._maskExtShow); this.detach("hide", this._maskExtHide) } }, _maskExtShow: function () {
        if (!f) {
            f = (new c.Node("<div class='ks-ext-mask'>")).prependTo(document.body); f.css({ position: "absolute",
                left: 0, top: 0, width: b.ie == 6 ? c.DOM.docWidth() : "100%", height: c.DOM.docHeight()
            }); b.ie == 6 && f.append("<iframe style='width:100%;height:expression(this.parentNode.offsetHeight);filter:alpha(opacity=0);z-index:-1;'>")
        } f.css({ "z-index": this.get("zIndex") - 1 }); a++; f.show()
    }, _maskExtHide: function () { a--; if (a <= 0) a = 0; a || f && f.hide() }, __destructor: function () { } 
    }; c.UIBase.Mask = e
}, { host: "uibase" });
KISSY.add("uibase-position", function (c) {
    function e() { } c.namespace("UIBase"); var f = document, b = c.Event; e.ATTRS = { x: {}, y: {}, xy: { setter: function (a) { var d = c.makeArray(a); if (d.length) { d[0] && this.set("x", d[0]); d[1] && this.set("y", d[1]) } return a } }, zIndex: { value: 9999 }, visible: { value: undefined} }; e.prototype = { __syncUI: function () { }, __renderUI: function () { var a = this.get("el"); a.addClass("ks-ext-position"); a.css("display", "") }, __bindUI: function () { }, _uiSetZIndex: function (a) {
        a !== undefined && this.get("el").css("z-index",
a)
    }, _uiSetX: function (a) { a !== undefined && this.get("el").offset({ left: a }) }, _uiSetY: function (a) { a !== undefined && this.get("el").offset({ top: a }) }, _uiSetVisible: function (a) { if (a !== undefined) { this.get("el").css("visibility", a ? "visible" : "hidden"); this[a ? "_bindKey" : "_unbindKey"](); this.fire(a ? "show" : "hide") } }, _bindKey: function () { b.on(f, "keydown", this._esc, this) }, _unbindKey: function () { b.remove(f, "keydown", this._esc, this) }, _esc: function (a) { a.keyCode === 27 && this.hide() }, move: function (a, d) {
        if (c.isArray(a)) {
            d = a[1];
            a = a[0]
        } this.set("xy", [a, d])
    }, show: function () { this._firstShow() }, _firstShow: function () { this.render(); this._realShow(); this._firstShow = this._realShow }, _realShow: function () { this.set("visible", true) }, hide: function () { this.set("visible", false) }, __destructor: function () { } 
    }; c.UIBase.Position = e
}, { host: "uibase" });
KISSY.add("uibase-shim", function (c) {
    function e() { } c.namespace("UIBase"); var f = c.Node; e.ATTRS = { shim: { value: true} }; e.prototype = { __syncUI: function () { }, __bindUI: function () { }, _uiSetShim: function (b) {
        var a = this.get("el"); if (b && !this.__shimEl) { this.__shimEl = new f("<iframe style='position: absolute;border: none;width: expression(this.parentNode.offsetWidth);top: 0;opacity: 0;filter: alpha(opacity=0);left: 0;z-index: -1;height: expression(this.parentNode.offsetHeight);'>"); a.prepend(this.__shimEl) } else if (!b &&
this.__shimEl) { this.__shimEl.remove(); delete this.__shimEl } 
    }, __renderUI: function () { }, __destructor: function () { } 
    }; c.UIBase.Shim = e
}, { host: "uibase" });
KISSY.add("uibase-stdmod", function (c) {
    function e() { } function f(d, k) { var g = d.get("contentEl"), h = d.get(k); if (!h) { h = (new a("<div class='" + b + k + "'>")).appendTo(g); d.set(k, h) } } c.namespace("UIBase"); var b = "ks-stdmod-", a = c.Node; e.ATTRS = { header: {}, body: {}, footer: {}, bodyStyle: {}, headerContent: { value: false }, bodyContent: { value: false }, footerContent: { value: false} }; e.HTML_PARSER = { header: "." + b + "header", body: "." + b + "body", footer: "." + b + "footer" }; e.prototype = { __bindUI: function () { }, __syncUI: function () { }, _setStdModContent: function (d,
k) { if (k !== false) if (c.isString(k)) this.get(d).html(k); else { this.get(d).html(""); this.get(d).append(k) } }, _uiSetBodyStyle: function (d) { d !== undefined && this.get("body").css(d) }, _uiSetBodyContent: function (d) { this._setStdModContent("body", d) }, _uiSetHeaderContent: function (d) { this._setStdModContent("header", d) }, _uiSetFooterContent: function (d) { this._setStdModContent("footer", d) }, __renderUI: function () { f(this, "header"); f(this, "body"); f(this, "footer") }, __destructor: function () { } 
    }; c.UIBase.StdMod = e
}, { host: "uibase" });

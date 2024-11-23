"use strict";(self.webpackChunkdocusaurus=self.webpackChunkdocusaurus||[]).push([[717],{328:(e,n,t)=>{t.r(n),t.d(n,{assets:()=>c,contentTitle:()=>a,default:()=>d,frontMatter:()=>s,metadata:()=>o,toc:()=>l});const o=JSON.parse('{"id":"how-to/ignoring-failures","title":"Ignoring Failures","description":"Sometimes a module might throw an exception, but we simply don\'t care as it\'s not that important, or a specific error might be expected.","source":"@site/docs/how-to/ignoring-failures.md","sourceDirName":"how-to","slug":"/how-to/ignoring-failures","permalink":"/ModularPipelines/docs/how-to/ignoring-failures","draft":false,"unlisted":false,"tags":[],"version":"current","frontMatter":{"title":"Ignoring Failures"},"sidebar":"tutorialSidebar","previous":{"title":"Hooks","permalink":"/ModularPipelines/docs/how-to/hooks"},"next":{"title":"Inheriting","permalink":"/ModularPipelines/docs/how-to/inheriting"}}');var i=t(4848),r=t(8453);const s={title:"Ignoring Failures"},a="Ignoring Failures",c={},l=[{value:"Example",id:"example",level:2}];function u(e){const n={code:"code",h1:"h1",h2:"h2",header:"header",p:"p",pre:"pre",...(0,r.R)(),...e.components};return(0,i.jsxs)(i.Fragment,{children:[(0,i.jsx)(n.header,{children:(0,i.jsx)(n.h1,{id:"ignoring-failures",children:"Ignoring Failures"})}),"\n",(0,i.jsx)(n.p,{children:"Sometimes a module might throw an exception, but we simply don't care as it's not that important, or a specific error might be expected."}),"\n",(0,i.jsxs)(n.p,{children:["We can hook into a ",(0,i.jsx)(n.code,{children:"ShouldIgnoreFailures"}),' method and plug in any conditional logic that means "that\'s okay, we can ignore that error."']}),"\n",(0,i.jsx)(n.h2,{id:"example",children:"Example"}),"\n",(0,i.jsx)(n.pre,{children:(0,i.jsx)(n.code,{className:"language-csharp",children:"public class MyModule : Module\n{\n    protected override Task<bool> ShouldIgnoreFailures(IPipelineContext context, Exception exception)\n    {\n        return (exception is ItemAlreadyExistsException).AsTask();\n    }\n\n    protected override Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)\n    {\n        // Do something\n    }\n}\n"})})]})}function d(e={}){const{wrapper:n}={...(0,r.R)(),...e.components};return n?(0,i.jsx)(n,{...e,children:(0,i.jsx)(u,{...e})}):u(e)}},8453:(e,n,t)=>{t.d(n,{R:()=>s,x:()=>a});var o=t(6540);const i={},r=o.createContext(i);function s(e){const n=o.useContext(r);return o.useMemo((function(){return"function"==typeof e?e(n):{...n,...e}}),[n,e])}function a(e){let n;return n=e.disableParentContext?"function"==typeof e.components?e.components(i):e.components||i:s(e.components),o.createElement(r.Provider,{value:n},e.children)}}}]);



function HtmlLinkJsonRenderer(params) {
    
    var jsonObj = JSON.parse(params.value);
    return `<a href="${jsonObj.link}">${jsonObj.displayText}</a>`;
}


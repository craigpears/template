window.openBlob = function(data) {
    var bytes = new Uint8Array(data.byteArray);
    var blob = new Blob([bytes.buffer], { type: 'application/pdf' });
    var url = URL.createObjectURL(blob);
    window.open(url, '_blank');
}
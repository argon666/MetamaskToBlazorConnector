export function showAlert() {
    import WalletConnectProvider from "@walletconnect/web3-provider";

    ////  Create WalletConnect Provider
    const provider = new WalletConnectProvider({
        infuraId: "27e484dcd9e3efcfd25a83a78777cdf1",
    });

    ////  Enable session (triggers QR Code modal)
    //await provider.enable();
    alert("aaaaa");
}
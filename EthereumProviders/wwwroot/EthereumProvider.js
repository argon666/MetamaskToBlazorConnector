window.EthereumProvider = {
    IsAvailable: () => {
        if (window.ethereum) return true;
        return false;
    },
    ReturnAccount: async () => {
        try {
            const accounts = await ethereum.request({ method: 'eth_requestAccounts' });
            return accounts[0];
        } catch (error) {
            return null;
        }

    },
    SendMessage: async (message) => {
        return new Promise(function (resolve, reject) {
            ethereum.send(JSON.parse(message), function (error, result) {
                resolve(JSON.stringify(result));
            });
        });
    }
}
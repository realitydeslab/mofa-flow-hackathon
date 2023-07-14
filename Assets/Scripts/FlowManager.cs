using System;
using DapperLabs.Flow.Sdk;
using DapperLabs.Flow.Sdk.Cadence;
using DapperLabs.Flow.Sdk.Crypto;
using DapperLabs.Flow.Sdk.DataObjects;
using DapperLabs.Flow.Sdk.Unity;
using DapperLabs.Flow.Sdk.WalletConnect;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using Convert = DapperLabs.Flow.Sdk.Cadence.Convert;

public class FlowManager : MonoBehaviour
{
    private void Start()
    {
        // Setup SDK to access TestNet
        FlowConfig flowConfig = new FlowConfig()
        {
            NetworkUrl = "https://rest-testnet.onflow.org/v1",  // testnet
            Protocol = FlowConfig.NetworkProtocol.HTTP
        };
        FlowSDK.Init(flowConfig);

        // Create WalletConnect wallet provider
        IWallet walletProvider = new WalletConnectProvider();
        walletProvider.Init(new WalletConnectConfig
        {
            ProjectId = "8b5de905401bb0f54acbcd8ec9733115",
            ProjectDescription = "MOFA Flow Hackathon Project",
            ProjectIconUrl = "https://walletconnect.com/meta/favicon.ico",
            ProjectName = "MOFA",
            ProjectUrl = "https://holokit.io"
        });

        FlowSDK.RegisterWalletProvider(walletProvider);

        Debug.Log("Hey");
        Login();
    }

    public void Login()
    {
        // Authenticate an account
        if (FlowSDK.GetWalletProvider().IsAuthenticated() == false)
        {
            FlowSDK.GetWalletProvider().Authenticate("", (string address) =>
            {
                Debug.Log($"Authentication succeeded with address: {address}");
            },
            () =>
            {
                Debug.Log("Authentication failed");
            });
        }
    }

    public void Logout()
    {
        FlowSDK.GetWalletProvider().Unauthenticate();
    }
}

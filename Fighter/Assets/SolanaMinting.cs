using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class SolanaMinting : MonoBehaviour
{
    // Update this to use Metaplex's minting endpoint (not the Solana RPC endpoint)
    public string mintEndpoint = "https://api.gameshift.dev"; // Replace with actual Metaplex minting endpoint

    public string authToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJrZXkiOiJiYWQ3Njc0Mi01YjQyLTRlYzQtYTk4Zi0zNzQwOGJiMjBhNjQiLCJzdWIiOiIxZGMzZjVhNy0yNWMyLTQ4NWYtYTBmMy03MWQ4MjA2OWNkZjUiLCJpYXQiOjE3MzMyMzI3MTR9.GoO5rGOxEN_2oHEgjaMCGsIES_CbSyAMDg5EJGeFR2M";

    private void Start()
    {
        StartMintNFT();
    }

    public void StartMintNFT()
    {
        StartCoroutine(MintNFTCoroutine());
    }

    private IEnumerator MintNFTCoroutine()
    {
        // Prepare data for the NFT (ensure metadata URI points to a valid metadata JSON)
        string metadataUri = "https://crimson-urgent-fish-395.mypinata.cloud/ipfs/QmRaSheNgKjgaQ3KKAhfxKJFNaRDd3A3o4ycmqjBSdQgHU"; // Replace with actual metadata URI
        string jsonData = JsonUtility.ToJson(new
        {
            name = "My NFT",
            symbol = "MNFT",
            uri = metadataUri,
            // Add other necessary fields for minting, like creator or attributes
        });

        // Prepare UnityWebRequest
        UnityWebRequest request = new UnityWebRequest(mintEndpoint, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Set headers
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + authToken);

        // Send request and handle response
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("NFT Minted Successfully: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error Minting NFT: " + request.error);
        }
    }
}

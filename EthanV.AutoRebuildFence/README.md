***このModはChatGPT（OpenAI）の支援を受けて開発されました。***

---

**概要**  
戦闘や火災で破壊されたフェンス・ゲート・家畜用出入口を自動的に再建します。  
同じ種類・素材のブループリントを自動で設置するため、見落としを防ぎます。  

襲撃の後、木の陰などに隠れたフェンスを直しそびれ、家畜が脱走することが頻発したため作成しました。  
自動再建築指示を出す対象はフェンス・ゲート・家畜用出入口のみで、壁やドアなどその他の建築物には関与しません。  
襲撃などで破壊されたコロニーを作り直す楽しみを完全になくすことはしたくなかったからです。  

**詳細**  
- プレイヤー所有のフェンス・ゲート・家畜用出入口のみ対象  
- より正確には、defName に「fence」「gate」「animalflap」を含むものが対象となります 
- 解体やブループリントのキャンセルは対象外  
- 瓦礫生成など他の破壊時処理との競合を避けるため、破壊から約5秒後に再建築指示を出します

**互換性**  
- RimWorld 1.6でテスト済み  
- Harmonyベース。`Thing.DeSpawn` を直接置き換えるModでなければ競合しないはずです

**ソースコード**  
- GitHubで公開中  
  https://github.com/EthanVarekoreski/EthanV/tree/master/EthanV.AutoRebuildFence  

**注意書き**  
冒頭に記したとおり、本Modの開発ではChatGPTの助力を受けています。コードは短く、動作内容は理解したうえで作成していますが、初めてのModであることや技術的未熟・AIの誤出力に起因する不具合が残っている可能性があります。問題や改善提案があれば、コメントでお知らせいただけると助かります。







***This mod was developed with assistance from ChatGPT (OpenAI).***

---

**Overview**  
Automatically rebuilds destroyed fences, gates, and animal flaps.  
When such a structure is destroyed by combat or fire, a blueprint of the same type and material is automatically placed for reconstruction. 
**Details**  
- Works only for player-owned fences, gates, and animal flaps.  
- More precisely, it targets any buildable whose defName contains “fence”, “gate”, or “animalflap”. 
- Deconstruction and blueprint cancellations are ignored.  
- To avoid conflicts with debris creation or other destruction-related processes, the rebuild command is issued about 5 seconds after destruction.

**Compatibility**  
- Tested with RimWorld 1.6  
- Harmony-based; it should not conflict with other construction mods unless they replace Thing.DeSpawn. 

- **Source**  
- Source available on GitHub:  
  https://github.com/EthanVarekoreski/EthanV/tree/master/EthanV.AutoRebuildFence

**Note**  
As stated above, this mod was created with help from ChatGPT. While the code is short and its behavior is understood by the author, this is the first released mod and there may still be issues due to limited experience or AI mistakes. If you run into problems or have suggestions, please leave a comment.

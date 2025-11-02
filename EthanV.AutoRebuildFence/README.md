**概要**  
戦闘や火災で破壊されたフェンス・ゲート・家畜用出入口を自動的に再建築します。
同じ種類・素材のブループリントを自動で設置し、再建を行います。

襲撃の後、木の陰などに隠れたフェンスを直しそびれ、家畜が脱走することが頻発したため作成しました。  
自動再建築指示を出す対象はフェンス・ゲート・家畜用出入口のみで、壁やドアなどその他の建築物には関与しません。  
襲撃などで破壊されたコロニーを作り直す楽しみを完全になくすことはしたくなかったからです。  

**詳細**  
- プレイヤー所有のフェンス・ゲート・家畜用出入口のみ対象  
- より正確には、defName に「fence」「gate」「animalflap」を含むものが対象となります 
- 解体やブループリントのキャンセルは対象外  
- 瓦礫生成など他の破壊時処理との競合を避けるため、破壊から約5秒後に再建築指示を出します

**前提MOD**
- Harmony(steam://url/CommunityFilePage/2009463077)

**互換性**  
- Harmonyベース。`Thing.DeSpawn` を直接置き換えるModでなければ競合しないはずです

**ソースコード**  
- GitHubで公開中  
  https://github.com/EthanVarekoreski/EthanV/tree/master/EthanV.AutoRebuildFence  

**注意書き**  
本Modの開発ではChatGPTの支援を一部利用しています。動作は理解したうえで作成していますが、不具合が残る可能性があります。ご意見・改善提案があればコメントでお知らせください。

---


**Overview**  
Automatically rebuilds destroyed fences, gates, and animal flaps.  
When such a structure is destroyed by combat or fire, a blueprint of the same type and material is automatically placed for reconstruction. 

**Details**  
- Works only for player-owned fences, gates, and animal flaps.  
- More precisely, it targets any buildable whose defName contains “fence”, “gate”, or “animalflap”. 
- Deconstruction and blueprint cancellations are ignored.  
- To avoid conflicts with debris creation or other destruction-related processes, the rebuild command is issued about 5 seconds after destruction.

**Required Mod**
- Harmony(steam://url/CommunityFilePage/2009463077)

**Compatibility**   
- Harmony-based; should not conflict with other construction mods unless they replace Thing.DeSpawn. 

**Source**  
- Source available on GitHub:  
  https://github.com/EthanVarekoreski/EthanV/tree/master/EthanV.AutoRebuildFence

**Note**  
This mod was developed with partial assistance from ChatGPT. The functionality was implemented with understanding, but some issues may remain. Feedback and suggestions are welcome in the comments.
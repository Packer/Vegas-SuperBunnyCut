# Super Bunny Cut
![Alt](https://github.com/Packer/Vegas-SuperBunnyCut/blob/main/SuperBunnyCut.png?raw=true)

# Features:
* Automated splitting of audio tracks during selected periods of silence


# Installation
[Download](https://github.com/Packer/Vegas-SuperBunnyCut/releases) version depending on your Vegas version
Example:
* `SBC13-#.#.#` for Vegas Pro 13 and older
* `SBC-#-#-#` for Vegas 15 and newer

Extract contents directly into your Vegas programs folder, the Vegas folder should contain the `.exe` of Vegas and a folder called `Script Menu`

*Note, not tested on any versions before 13 and after 15*

# How to Use
* Select an audio track(s) you wish to split
* Goto Top Menu bar `Tools > Scriptings > SuperBunnyCut`
* Super Bunny Cut window should pop out, See settings below
* Process - Splits the selected audio tracks with settings
* Sample Media - A debugging window will pop up with infomation on the media and selected track used for debugging...

*NOTE, Settings will not save between sessions (Program running) but will save between uses, make sure to note your settings*

| Setting    | Description |
| -------- | ------- |
| Volume Threshold  | The maximum volume (In percentage) the script will look for as 'silence' |
| Minimum Silence Time | The minimum period of 'silence' (In Seconds) to be a valid spot to split, too low will split at every brief silence and too high won't find many splits |
| Split Point | The point in the 'silence' range (In percentage) the split is placed |

### Additional Uses
* Setting the Split Point to `0%` and splitting all selected audio tracks, then running the process again with Split Point to `100%` on the newely created splitted tracks will create splits at each end of the silence area

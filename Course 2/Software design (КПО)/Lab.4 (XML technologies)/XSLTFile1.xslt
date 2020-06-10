<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="text" indent="yes"/>

  <xsl:template match="schedule">
    <xsl:apply-templates select="day"/>
  </xsl:template>

  <xsl:template match="day">
    <xsl:value-of select="@weekday"/>
    <xsl:text>:&#10;</xsl:text>
    <xsl:apply-templates select="subject"/>
  </xsl:template>

  <xsl:template match="subject">
    <xsl:text>(</xsl:text>
    <xsl:value-of select="timeFrame/start"/>
    <xsl:text>-</xsl:text>
    <xsl:value-of select="timeFrame/end"/>
    <xsl:text>) </xsl:text>
    <xsl:value-of select="class"/>
    <xsl:text> </xsl:text>
    <xsl:value-of select="name" disable-output-escaping="yes"/>
    <xsl:text> /</xsl:text>
    <xsl:value-of select="@type"/>
    <xsl:text>/ </xsl:text>
    <xsl:text> |</xsl:text>
    <xsl:value-of select="teacher"/>
    <xsl:text>|</xsl:text>
    <xsl:text>&#10;</xsl:text>
  </xsl:template>
</xsl:stylesheet>